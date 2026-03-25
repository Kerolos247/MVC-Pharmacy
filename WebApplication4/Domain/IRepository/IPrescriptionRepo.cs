using WebApplication4.Domain.Models;
namespace WebApplication4.Domain.IRepository
{
    public interface IPrescriptionRepo
    {
        Task<Prescription?> GetByIdAsync(int id);
        Task<IEnumerable<Prescription>> GetAllAsync();
        Task AddAsync(Prescription prescription);
        Task UpdateAsync(Prescription prescription);
        Task DeleteAsync(Prescription prescription);

        Task<bool> ExistsByPatientIdAsync(int patientId);

        Task<int> GetPrescriptionCountAsync();
        Task<int> GetPharmacistsCount();

    }
}
