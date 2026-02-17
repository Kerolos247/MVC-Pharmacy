using WebApplication4.Application.Common.Results;
using WebApplication4.Application.Dto.Prescription;
using WebApplication4.Application.IServices;
using WebApplication4.Domain.Models;

namespace WebApplication4.Domain.IRepository
{
    public interface IPrescriptionRepo
    {
        Task<Prescription?> GetByIdAsync(int id);
        Task<List<Prescription>> GetAllAsync();
        Task AddAsync(Prescription prescription);
        Task UpdateAsync(Prescription prescription);
        Task DeleteAsync(Prescription prescription);

        Task<bool> ExistsByPatientIdAsync(int patientId);

    }
}
