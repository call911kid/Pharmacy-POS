using Newtonsoft.Json;

namespace DrugSearch.Models
{
    public class FdaResponse
    {
        [JsonProperty("results")]
        public DrugLabel[] Results { get; set; } = Array.Empty<DrugLabel>();
    }

    public class DrugLabel
    {
        [JsonIgnore]
        public string DataSource { get; set; } = "";

        [JsonProperty("openfda")]
        public OpenFda? OpenFda { get; set; }

        [JsonProperty("active_ingredient")]
        public string[]? ActiveIngredient { get; set; }

        [JsonProperty("purpose")]
        public string[]? Purpose { get; set; }

        [JsonProperty("indications_and_usage")]
        public string[]? IndicationsAndUsage { get; set; }

        [JsonProperty("dosage_and_administration")]
        public string[]? DosageAndAdministration { get; set; }

        [JsonProperty("warnings")]
        public string[]? Warnings { get; set; }

        [JsonProperty("do_not_use")]
        public string[]? DoNotUse { get; set; }

        [JsonProperty("ask_doctor")]
        public string[]? AskDoctor { get; set; }

        [JsonProperty("when_using")]
        public string[]? WhenUsing { get; set; }

        [JsonProperty("stop_use")]
        public string[]? StopUse { get; set; }

        [JsonProperty("overdosage")]
        public string[]? Overdosage { get; set; }

        [JsonProperty("pregnancy_or_breast_feeding")]
        public string[]? PregnancyOrBreastFeeding { get; set; }

        [JsonProperty("storage_and_handling")]
        public string[]? StorageAndHandling { get; set; }

        [JsonProperty("inactive_ingredient")]
        public string[]? InactiveIngredient { get; set; }

        public string DisplayName =>
            OpenFda?.BrandName?.Length > 0
                ? $"{OpenFda.BrandName[0]}  —  {OpenFda.GenericName?[0] ?? ""}"
                : "Unknown";
    }

    public class OpenFda
    {
        [JsonProperty("brand_name")]
        public string[]? BrandName { get; set; }

        [JsonProperty("generic_name")]
        public string[]? GenericName { get; set; }

        [JsonProperty("manufacturer_name")]
        public string[]? ManufacturerName { get; set; }

        [JsonProperty("product_type")]
        public string[]? ProductType { get; set; }

        [JsonProperty("route")]
        public string[]? Route { get; set; }

        [JsonProperty("substance_name")]
        public string[]? SubstanceName { get; set; }
    }
}
