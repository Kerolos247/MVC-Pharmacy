using Microsoft.EntityFrameworkCore;
using WebApplication4.Application.Dto;
using WebApplication4.Domain.IRepository;
using WebApplication4.Domain.Models;
using WebApplication4.Infrastructure.DB;

namespace WebApplication4.Infrastructure.Repository
{
    public class PatientRepo : IPatientRepo
    {
        private readonly ApplicationDbContext _context;

        public PatientRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await _context.Patients
                .Include(p => p.Prescriptions)
                .FirstOrDefaultAsync(p => p.PatientId == id);
        }


        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients
                                 .Include(p => p.Prescriptions)
                                 .ToListAsync();
        }

        public async Task<Patient> CreateAsync(RequestCreatePatient dto)
        {
            var patient = new Patient
            {
                FullName = dto.FullName,
                Phone = dto.Phone,
                Address = dto.Address
            };

            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();

            return patient;
        }

        public async Task<Patient?> UpdateAsync(int id, UpdatePatientDto dto)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return null;

            if (!string.IsNullOrEmpty(dto.FullName)) patient.FullName = dto.FullName;
            if (!string.IsNullOrEmpty(dto.Phone)) patient.Phone = dto.Phone;
            if (!string.IsNullOrEmpty(dto.Address)) patient.Address = dto.Address;

            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return false;

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
