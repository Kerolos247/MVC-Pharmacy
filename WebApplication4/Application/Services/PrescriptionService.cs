using WebApplication4.Application.Dto;
using WebApplication4.Application.IServices;
using WebApplication4.Domain.IRepository;
using WebApplication4.Domain.Models;

namespace WebApplication4.Application.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepo _prescriptionRepo;

        public PrescriptionService(IPrescriptionRepo prescriptionRepo)
        {
            _prescriptionRepo = prescriptionRepo;
        }

      
        public async Task<List<Prescription>> GetAllPrescriptionsAsync()
        {
            return await _prescriptionRepo.GetAllPrescriptionsAsync();
        }

       
        public async Task<Prescription?> GetByIdAsync(int id)
        {
            return await _prescriptionRepo.GetByIdAsync(id);
        }

       
        public async Task<Prescription> CreateAsync(RequestCreatePrescription dto)
        {
            return await _prescriptionRepo.CreateAsync(dto);
        }

      
        public async Task<Prescription?> UpdateAsync(int id, UpdatePrescriptionDto dto)
        {
            return await _prescriptionRepo.UpdateAsync(id, dto);
        }

       
        public async Task<bool> DeleteAsync(int id)
        {
            return await _prescriptionRepo.DeleteAsync(id);
        }

       
        public async Task<ResponseCostDto> PayAsync(int id, IPayment payment)
        {
            return await _prescriptionRepo.PayAsync(id, payment);
        }
    }
}
