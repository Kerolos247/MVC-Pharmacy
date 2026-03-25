using WebApplication4.Domain.Models;
namespace WebApplication4.Domain.IRepository
{
    public interface IPrescriptionUploadRepo
    {
        Task AddAsync(PrescriptionUpload prescription);
        Task<ICollection<PrescriptionUpload>> GetAllAsync();
        Task DeleteAsync(int id);
      
    }
}
