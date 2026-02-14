using WebApplication4.Application.Dto;
using WebApplication4.Domain.Models;

namespace WebApplication4.Domain.IRepository
{
    public interface IPatientRepo
    {
        Task<Patient?> GetByIdAsync(int id);
        Task<List<Patient>> GetAllPatientsAsync();
        Task<Result<bool>> CreateAsync(RequestCreatePatient dto);
        Task<Result<bool>> UpdateAsync(int id, UpdatePatientDto dto);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
