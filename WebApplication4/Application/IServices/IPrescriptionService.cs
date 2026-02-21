using WebApplication4.Application.Common.Results;
using WebApplication4.Application.Dto.Prescription;
using WebApplication4.Application.Services.PaymentStrategies;
using WebApplication4.Domain.Models;

namespace WebApplication4.Application.IServices
{
    public interface IPrescriptionService
    {
        Task<Result<List<Prescription>>> GetAllPrescriptionsAsync();
        Task<Result<Prescription?>> GetByIdAsync(int id);
        Task<Result<bool>> CreateAsync(RequestCreatePrescription dto);
        Task<Result<bool>> UpdateAsync(int id, UpdatePrescriptionDto dto, string pharmacistId);
        Task<Result<bool>> DeleteAsync(int id);
        Task<Result<ResponseCostDto>> PayAsync(int id, IPayment payment);
    }
}
