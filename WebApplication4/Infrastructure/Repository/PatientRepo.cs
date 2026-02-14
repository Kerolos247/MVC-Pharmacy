using Microsoft.EntityFrameworkCore;
using WebApplication4.Application.Dto;
using WebApplication4.Application.IServices;
using WebApplication4.Domain.IRepository;
using WebApplication4.Domain.Models;
using WebApplication4.Infrastructure.DB;

namespace WebApplication4.Infrastructure.Repository
{
    public class PatientRepo : IPatientRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidationService _validationService;

        public PatientRepo(ApplicationDbContext context,IValidationService validationService)
        {
            _context = context;
            _validationService = validationService;
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            //Use AsNoTracking to improve performance since we only need to read the patient and not track changes to it
            return await _context.Patients.AsNoTracking()
                .Include(p => p.Prescriptions)
                .FirstOrDefaultAsync(p => p.PatientId == id);
        }


        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            //Use AsNoTracking to improve performance since we only need to read the patients and not track changes to them
            return await _context.Patients.AsNoTracking()
                                 .Include(p => p.Prescriptions)
                                 .ToListAsync();
        }

        public async Task<Result<bool>> CreateAsync(RequestCreatePatient dto)
        {
            //CHeck The phone number is unique for patients
            if (await _validationService.PhoneExistsAsync<Patient>(dto.Phone, "PatientId"))
                return Result<bool>.Failure("Phone already exists");


            var patient = new Patient
            {
                FullName = dto.FullName,
                Phone = dto.Phone,
                Address = dto.Address
            };

            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> UpdateAsync(int id, UpdatePatientDto dto)
        {
            //Find the patient by ID
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return Result<bool>.Failure("Not Found Patient");

            //Check if the phone number is unique for patients, excluding the current patient being updated
            if (await _validationService.PhoneExistsAsync<Patient>(dto.Phone, "PatientId", id))
                return Result<bool>.Failure("Phone already exists");



            if (!string.IsNullOrEmpty(dto.FullName)) patient.FullName = dto.FullName;
            if (!string.IsNullOrEmpty(dto.Phone)) patient.Phone = dto.Phone;
            if (!string.IsNullOrEmpty(dto.Address)) patient.Address = dto.Address;

            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            //Find the patient by ID
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return Result<bool>.Failure("Not Found Patient");

            //Check if the patient has any prescriptions, if so, we cannot delete the patient
            bool res =await _context.Prescriptions.AnyAsync(p => p.PatientId == id);
            if(res)
                return Result<bool>.Failure("Cannot delete patient with existing prescriptions");

            //If the patient has no prescriptions, we can safely delete it
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
       
    }
}
