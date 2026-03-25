using WebApplication4.Domain.Models;
namespace WebApplication4.Domain.IRepository
{
    public interface IPatientRepo
    {
        Task<Patient?> GetByIdAsync(int id);
        Task<IEnumerable<Patient>> GetAllAsync();
        Task AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task DeleteAsync(Patient patient);
        Task<int> GetPatientCountAsync();

    }
}
