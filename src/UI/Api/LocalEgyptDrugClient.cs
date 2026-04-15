using Newtonsoft.Json;
using DrugSearch.Models;

namespace DrugSearch.Api
{
    public static class LocalEgyptDrugClient
    {
        private static readonly object SyncRoot = new();
        private static List<EgyptDrugRecord>? _cache;

        public static DrugLabel[] SearchByName(string query, int limit = 20)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Array.Empty<DrugLabel>();
            }

            var data = EnsureLoaded();
            if (data.Count == 0)
            {
                return Array.Empty<DrugLabel>();
            }

            string loweredQuery = query.Trim().ToLowerInvariant();
            return data
                .Where(d => !string.IsNullOrWhiteSpace(d.TradeName)
                    && d.TradeName!.ToLowerInvariant().Contains(loweredQuery))
                .Take(limit)
                .Select(ToDrugLabel)
                .ToArray();
        }

        private static List<EgyptDrugRecord> EnsureLoaded()
        {
            if (_cache is not null)
            {
                return _cache;
            }

            lock (SyncRoot)
            {
                if (_cache is not null)
                {
                    return _cache;
                }

                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "egypt_drugs_database.json");
                if (!File.Exists(path))
                {
                    _cache = new List<EgyptDrugRecord>();
                    return _cache;
                }

                string json = File.ReadAllText(path);
                _cache = JsonConvert.DeserializeObject<List<EgyptDrugRecord>>(json) ?? new List<EgyptDrugRecord>();
                return _cache;
            }
        }

        private static DrugLabel ToDrugLabel(EgyptDrugRecord record)
        {
            string concentrationText = record.Concentration.HasValue
                ? $"{record.Concentration.Value} {record.Unit ?? string.Empty}".Trim()
                : (record.Unit ?? string.Empty);

            return new DrugLabel
            {
                OpenFda = new OpenFda
                {
                    BrandName = new[] { record.TradeName ?? "Unknown" },
                    GenericName = new[] { record.GenericName ?? string.Empty },
                    ManufacturerName = new[] { record.Company ?? string.Empty },
                    Route = new[] { record.DosageForm ?? string.Empty }
                },
                ActiveIngredient = string.IsNullOrWhiteSpace(record.GenericName) ? null : new[] { record.GenericName },
                IndicationsAndUsage = string.IsNullOrWhiteSpace(record.DrugType)
                    ? null
                    : new[] { record.DrugType },
                DosageAndAdministration = string.IsNullOrWhiteSpace(record.DosageForm) && string.IsNullOrWhiteSpace(concentrationText)
                    ? null
                    : new[] { $"{record.DosageForm ?? string.Empty} {concentrationText}".Trim() },
                StorageAndHandling = string.IsNullOrWhiteSpace(record.Company)
                    ? null
                    : new[] { $"Company: {record.Company}" },
                DataSource = "Local"
            };
        }
    }
}
