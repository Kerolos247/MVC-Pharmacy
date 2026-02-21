using WebApplication4.Application.Dto.Patient;
using WebApplication4.Domain.Models;

namespace WebApplication4.Domain.IRepository
{
    public interface IFeedBackRepo
    {
        Task AddAsync(PatientFeedback feedBack);
        Task<List<PatientFeedback>> GetAllAsync();
        Task<PatientFeedback> GetByIdAsync(int id);
        Task DeleteAsync(int id);

    }
}
