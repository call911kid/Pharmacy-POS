using Newtonsoft.Json;

namespace PrescriptionScanner.Models
{
    public class Medicine
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("dosage")]
        public string? Dosage { get; set; }

        [JsonProperty("frequency")]
        public string? Frequency { get; set; }

        [JsonProperty("duration")]
        public string? Duration { get; set; }

        [JsonProperty("instructions")]
        public string? Instructions { get; set; }
    }
}
