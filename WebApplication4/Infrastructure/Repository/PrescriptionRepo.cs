using Microsoft.EntityFrameworkCore;
using WebApplication4.Domain.IRepository;
using WebApplication4.Domain.Models;
using WebApplication4.Infrastructure.DB;

namespace WebApplication4.Infrastructure.Repository
{
    public class PrescriptionRepo : IPrescriptionRepo
    {
        private readonly ApplicationDbContext _context;

        public PrescriptionRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Prescription?> GetByIdAsync(int id)
        {
            return await _context.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Pharmacist)
                .Include(p => p.PrescriptionItems)
                .FirstOrDefaultAsync(p => p.PrescriptionId == id);
        }

        public async Task<List<Prescription>> GetAllAsync()
        {
            return await _context.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.PrescriptionItems)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(Prescription prescription)
        {
            await _context.Prescriptions.AddAsync(prescription);
        }

        public Task UpdateAsync(Prescription prescription)
        {
            _context.Prescriptions.Update(prescription);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Prescription prescription)
        {
            _context.Prescriptions.Remove(prescription);
            return Task.CompletedTask;
        }

        public Task<bool> ExistsByPatientIdAsync(int patientId)
        {
            return _context.Prescriptions.AnyAsync(p => p.PatientId == patientId);
        }
    }
}
