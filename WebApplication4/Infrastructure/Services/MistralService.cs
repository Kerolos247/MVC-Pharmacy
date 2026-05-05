using System.Text;
using System.Text.Json;
using WebApplication4.Application.IServices;
using WebApplication4.Application.Dto.ChatAi;

namespace WebApplication4.Infrastructure.Services
{
    public class MistralService : IChatAiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _fastApiUrl = "http://127.0.0.1:8000/chat";

        public MistralService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> Ask(string question)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var payload = new { message = question };
                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(payload),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync(_fastApiUrl, jsonContent);

                if (!response.IsSuccessStatusCode)
                    return "الموديل غير متصل.";

                var responseData = await response.Content.ReadAsStringAsync();
                var apiResult = JsonSerializer.Deserialize<FastApiResponse>(responseData);

                return apiResult?.response ?? "مفيش رد.";
            }
            catch (Exception ex)
            {
                return "خطأ في الاتصال: " + ex.Message;
            }
        }
    }
}