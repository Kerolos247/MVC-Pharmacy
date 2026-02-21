using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
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

           
            var url = $"https://lynelle-coyish-unfrivolously.ngrok-free.dev/sentiment?text={Uri.EscapeDataString(text)}";

           
            var response = await _client.PostAsync(url, null);

            var resultJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new SentimentResponseDto
                {
                    Label = "error",
                    Score = 0
                };
            }

            var result = JsonSerializer.Deserialize<SentimentResponseDto>(
                resultJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            var responseDto = new SentimentResponseDto
            {
                Label = result?.Label ?? "unknown",
                Score = result?.Score ?? 0
            };

            _cache.Set(text, responseDto, TimeSpan.FromMinutes(10));
            return responseDto;
        }
    }
}