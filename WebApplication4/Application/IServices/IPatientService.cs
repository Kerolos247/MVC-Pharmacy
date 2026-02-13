using WebApplication4.Application.Dto;
using WebApplication4.Domain.Models;

namespace WebApplication4.Application.IServices
{
    public interface IPatientService
    {
        Task<List<Patient>> GetAllPatientsAsync();
        Task<Patient?> GetByIdAsync(int id);
        Task<Patient> CreateAsync(RequestCreatePatient dto);
        Task<Patient?> UpdateAsync(int id, UpdatePatientDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
