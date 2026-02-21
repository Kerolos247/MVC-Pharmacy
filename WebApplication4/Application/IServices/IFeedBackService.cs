using WebApplication4.Application.Common.Results;
using WebApplication4.Application.Dto.Patient;  
namespace WebApplication4.Application.IServices
{
    public interface IFeedBackService
    {
        Task<Result<bool>> AddAsync(PatientFeedbackDto feedBack);

        Task<Result<List<PatientFeedbackDto>>> GetAllAsync();

        Task<Result<PatientFeedbackDto?>> GetByIdAsync(int id);

        Task<Result<bool>>DeleteAsync(int id);
    }
}
