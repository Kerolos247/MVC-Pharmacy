using WebApplication4.Dto;
using WebApplication4.Models;

namespace WebApplication4.Service_Layer.Interface
{
    public interface IPatientService
    {
        Task<Patient?> GetByIdAsync(int id);

        
        Task<List<Patient>> GetAllPatientsAsync();

       
        Task<Patient> CreateAsync(RequestCreatePatient dto);

       
        Task<Patient?> UpdateAsync(int id, UpdatePatientDto dto);

      
        Task<bool> DeleteAsync(int id);
    }
}
