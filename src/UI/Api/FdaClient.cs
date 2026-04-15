using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DrugSearch.Models;

namespace DrugSearch.Api
{
    public static class FdaClient
    {
        private static readonly HttpClient _http = new HttpClient();
        private const string BaseUrl = "https://api.fda.gov/drug/label.json";

        public static async Task<DrugLabel[]> SearchByNameAsync(string drugName, int limit = 15)
        {
            if (string.IsNullOrWhiteSpace(drugName))
                return Array.Empty<DrugLabel>();

            string encoded = WebUtility.UrlEncode(drugName.Trim());
            string url = $"{BaseUrl}?search=openfda.brand_name:{encoded}&limit={limit}";

            var response = await _http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return Array.Empty<DrugLabel>();
                response.EnsureSuccessStatusCode();
            }

            string json = await response.Content.ReadAsStringAsync();
            var root = JsonConvert.DeserializeObject<FdaResponse>(json);
            return root?.Results ?? Array.Empty<DrugLabel>();
        }
    }
}
