using WebApplication4.Application.Dto.SentimentModel;

namespace WebApplication4.Application.IServices
{
    public interface ISentimentService
    {
        Task<SentimentResponseDto> AnalyzeAsync(string text);
    }
}
