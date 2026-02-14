using WebApplication4.Application.Dto;
using WebApplication4.Domain.Models;

namespace WebApplication4.Application.IServices
{
    public interface IPrescriptionService
    {
        Task<List<Prescription>> GetAllPrescriptionsAsync();
        Task<Prescription?> GetByIdAsync(int id);
        Task<Result<bool>> CreateAsync(RequestCreatePrescription dto);
        Task<Result<bool>> UpdateAsync(int id, UpdatePrescriptionDto dto, string pharmacistId);
        Task<Result<bool>> DeleteAsync(int id);
        Task<ResponseCostDto> PayAsync(int id, IPayment payment);
    }
}
