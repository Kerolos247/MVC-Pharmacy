using WebApplication4.Application.Common.Results;
using WebApplication4.Application.Dto.Patient;
using WebApplication4.Domain.Models;

namespace WebApplication4.Application.IServices
{
    public interface IPatientService
    {
        Task<List<Patient>> GetAllPatientsAsync();
        Task<Patient?> GetByIdAsync(int id);
        Task<Result<bool>> CreateAsync(RequestCreatePatient dto);
        Task<Result<bool>> UpdateAsync(int id, UpdatePatientDto dto);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
