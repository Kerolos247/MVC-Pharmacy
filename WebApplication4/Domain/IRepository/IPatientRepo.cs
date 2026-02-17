using WebApplication4.Application.Common.Results;
using WebApplication4.Application.Dto.Patient;
using WebApplication4.Domain.Models;

namespace WebApplication4.Domain.IRepository
{
    public interface IPatientRepo
    {
        Task<Patient?> GetByIdAsync(int id);
        Task<List<Patient>> GetAllAsync();
        Task AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task DeleteAsync(Patient patient);
       
    }
}
