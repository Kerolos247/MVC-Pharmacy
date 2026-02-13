using WebApplication4.Application.Dto;
using WebApplication4.Application.IServices;
using WebApplication4.Domain.Models;

namespace WebApplication4.Domain.IRepository
{
    public interface IPrescriptionRepo
    {
        Task<Prescription?> GetByIdAsync(int id);
        Task<List<Prescription>> GetAllPrescriptionsAsync();
        Task<Prescription> CreateAsync(RequestCreatePrescription dto);
        Task<Prescription?> UpdateAsync(int id, UpdatePrescriptionDto dto);
        Task<bool> DeleteAsync(int id);

        Task<ResponseCostDto> PayAsync(int id, IPayment payment);

    }
}
