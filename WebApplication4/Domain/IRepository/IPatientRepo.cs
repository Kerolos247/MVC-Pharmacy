using WebApplication4.Application.Dto;
using WebApplication4.Domain.Models;

namespace WebApplication4.Domain.IRepository
{
    public interface IPatientRepo
    {
        Task<Patient?> GetByIdAsync(int id);
        Task<List<Patient>> GetAllPatientsAsync();
        Task<Patient> CreateAsync(RequestCreatePatient dto);
        Task<Patient?> UpdateAsync(int id, UpdatePatientDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
