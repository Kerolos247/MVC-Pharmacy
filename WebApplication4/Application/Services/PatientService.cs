using WebApplication4.Domain.IRepository;
using WebApplication4.Domain.Models;
using WebApplication4.Application.IServices;
using WebApplication4.Application.Common.Results;
using WebApplication4.Application.Dto.Patient;

namespace WebApplication4.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepo _patientRepo;

        public PatientService(IPatientRepo patientRepo)
        {
            _patientRepo = patientRepo;
        }

       
        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _patientRepo.GetAllPatientsAsync();
        }

       
        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await _patientRepo.GetByIdAsync(id);
        }

      
        public async Task<Result<bool>> CreateAsync(RequestCreatePatient dto)
        {
            return await _patientRepo.CreateAsync(dto);
        }

       
        public async Task<Result<bool>> UpdateAsync(int id, UpdatePatientDto dto)
        {
            return await _patientRepo.UpdateAsync(id, dto);
        }

       
        public async Task<Result<bool>> DeleteAsync(int id)
        {
            return await _patientRepo.DeleteAsync(id);
        }
    }
}
