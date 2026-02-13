using WebApplication4.Application.Dto;
using WebApplication4.Domain.Models;

namespace WebApplication4.Application.IServices
{
    public interface IPrescriptionService
    {
        Task<List<Prescription>> GetAllPrescriptionsAsync();
        Task<Prescription?> GetByIdAsync(int id);
        Task<Prescription> CreateAsync(RequestCreatePrescription dto);
        Task<Prescription?> UpdateAsync(int id, UpdatePrescriptionDto dto);
        Task<bool> DeleteAsync(int id);
        Task<ResponseCostDto> PayAsync(int id, IPayment payment);
    }
}
