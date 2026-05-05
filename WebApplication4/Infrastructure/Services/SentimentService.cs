using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApplication4.Application.Dto.SentimentModel;
using WebApplication4.Application.IServices;

namespace WebApplication4.Infrastructure.Services
{
    public class SentimentService : ISentimentService
    {
        private readonly HttpClient _client;
        private readonly IMemoryCache _cache;

        public SentimentService(IMemoryCache cache)
        {
            _client = new HttpClient();
            _cache = cache;
        }

        public async Task<SentimentResponseDto> AnalyzeAsync(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new SentimentResponseDto { Label = "invalid", Score = 0 };

            if (_cache.TryGetValue(text, out SentimentResponseDto cached))
                return cached;

            // إعدادات Hugging Face
            var modelId = "kerolos1/analysis-of-Egyptian-sentiments";
            var url = $"https://api-inference.huggingface.co/models/{modelId}";
            var hfToken = "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"; // حط التوكن بتاعك هنا

            // تجهيز الـ Payload (جسم الطلب)
            var payload = new { inputs = text };
            var jsonContent = new StringContent(JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json");

            // إضافة الـ Authorization Header
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", hfToken);

            try
            {
                var response = await _client.PostAsync(url, jsonContent);
                var resultJson = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    // لو الموديل بيحمل (Loading) هيرجع 503، ممكن تتعامل معاها هنا
                    return new SentimentResponseDto { Label = "error", Score = 0 };
                }

                // Hugging Face بترجع List<List<HuggingFaceResponse>>
                var results = JsonSerializer.Deserialize<List<List<HuggingFaceResponse>>>(
                    resultJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                // هناخد أول عنصر في أول مصفوفة (الأعلى Score غالباً)
                var topResult = results?[0]?.OrderByDescending(x => x.Score).FirstOrDefault();

                var responseDto = new SentimentResponseDto
                {
                    Label = topResult?.Label ?? "unknown",
                    Score = (float)(topResult?.Score ?? 0)
                };

                _cache.Set(text, responseDto, TimeSpan.FromMinutes(10));
                return responseDto;
            }
            catch (Exception)
            {
                return new SentimentResponseDto { Label = "exception", Score = 0 };
            }
        }
    }
    public class HuggingFaceResponse
    {
        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("score")]
        public double Score { get; set; }
    }
}