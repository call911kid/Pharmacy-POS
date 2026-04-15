using Newtonsoft.Json;

namespace PrescriptionScanner.Models
{
    public class PrescriptionData
    {
        [JsonProperty("patient_name")]
        public string? PatientName { get; set; }

        [JsonProperty("doctor_name")]
        public string? DoctorName { get; set; }

        [JsonProperty("date")]
        public string? Date { get; set; }

        [JsonProperty("medicines")]
        public Medicine[]? Medicines { get; set; }

        [JsonProperty("additional_notes")]
        public string? Notes { get; set; }
    }
}
