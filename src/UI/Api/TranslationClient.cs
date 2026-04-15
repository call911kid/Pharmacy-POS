using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DrugSearch.Api
{
    public static class TranslationClient
    {
        private static readonly HttpClient _http = new HttpClient();

        public static async Task<string> TranslateAsync(string text, string targetLang = "ar")
        {
            if (string.IsNullOrWhiteSpace(text)) return text;

            if (text.Length <= 500)
                return await TranslateChunkAsync(text, targetLang);

            var chunks = SplitIntoChunks(text, 450);
            var results = new StringBuilder();
            foreach (var chunk in chunks)
            {
                string translated = await TranslateChunkAsync(chunk, targetLang);
                results.Append(translated).Append(" ");
                await Task.Delay(150);
            }
            return results.ToString().Trim();
        }

        private static async Task<string> TranslateChunkAsync(string text, string targetLang)
        {
            string encoded = WebUtility.UrlEncode(text);
            string url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=en&tl={targetLang}&dt=t&q={encoded}";

            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();

            JArray root = JArray.Parse(json);
            if (root.Count == 0 || root[0] is not JArray sentences)
            {
                return text;
            }

            var builder = new StringBuilder();
            foreach (var sentence in sentences)
            {
                if (sentence is JArray arr && arr.Count > 0)
                {
                    var part = arr[0]?.Value<string>();
                    if (!string.IsNullOrWhiteSpace(part))
                    {
                        builder.Append(part);
                    }
                }
            }

            string translated = builder.ToString().Trim();
            return string.IsNullOrEmpty(translated) ? text : translated;
        }

        private static List<string> SplitIntoChunks(string text, int maxLen)
        {
            var chunks = new List<string>();
            int start = 0;
            while (start < text.Length)
            {
                int end = Math.Min(start + maxLen, text.Length);
                if (end < text.Length)
                {
                    int dot = text.LastIndexOf('.', end, end - start);
                    if (dot > start) end = dot + 1;
                }
                chunks.Add(text.Substring(start, end - start).Trim());
                start = end;
            }
            return chunks;
        }
    }
}
