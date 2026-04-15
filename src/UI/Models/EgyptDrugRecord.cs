using Newtonsoft.Json;

namespace DrugSearch.Models
{
    public sealed class EgyptDrugRecord
    {
        [JsonProperty("ID")]
        public int Id { get; set; }

        [JsonProperty("TradeName")]
        public string? TradeName { get; set; }

        [JsonProperty("GenericName")]
        public string? GenericName { get; set; }

        [JsonProperty("Concentration")]
        public decimal? Concentration { get; set; }

        [JsonProperty("Unit")]
        public string? Unit { get; set; }

        [JsonProperty("DosageForm")]
        public string? DosageForm { get; set; }

        [JsonProperty("Company")]
        public string? Company { get; set; }

        [JsonProperty("DrugType")]
        public string? DrugType { get; set; }
    }
}
