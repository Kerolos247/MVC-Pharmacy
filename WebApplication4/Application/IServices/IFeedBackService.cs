using WebApplication4.Application.Common.Results;
using WebApplication4.Application.Dto.Patient;  
using WebApplication4.Domain.Models;    
namespace WebApplication4.Application.IServices
{
    public interface IFeedBackService
    {
        Task<Result<int>> AddAsync(PatientFeedbackDto feedBack);

        Task<Result<List<PatientFeedbackDto>>> GetAllAsync();

        Task<Result<PatientFeedbackDto?>> GetByIdAsync(int id);

        Task<Result<bool>>DeleteAsync(int id);


        Task<Result<bool>> SentimentAnalysis(FeedbackSentiment sentiment, int Id);
    }
}
