using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PrescriptionScanner.Models;

namespace PrescriptionScanner.Api
{
    public class ApiClient
    {
        private static readonly HttpClient _http = new HttpClient();

        private const string ApiUrl = "https://ai.prescriptionreader.in/api/analyze";
        private const string ModelName = "gemini-2.5-flash-lite";

        public static async Task<PrescriptionData?> AnalyzeAsync(string base64Image)
        {
            var payload = new
            {
                image_base64 = base64Image,
                model = ModelName
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(payload),
                Encoding.UTF8,
                "application/json");

            _http.DefaultRequestHeaders.Clear();
            _http.DefaultRequestHeaders.Add("accept", "*/*");
            _http.DefaultRequestHeaders.Add("origin", "https://prescriptionreader.in");
            _http.DefaultRequestHeaders.Add("referer", "https://prescriptionreader.in/");

            var response = await _http.PostAsync(ApiUrl, content);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var root = JObject.Parse(json);

            if (root["success"]?.Value<bool>() == true)
            {
                return root["data"]?.ToObject<PrescriptionData>();
            }

            return null;
        }
    }
}
