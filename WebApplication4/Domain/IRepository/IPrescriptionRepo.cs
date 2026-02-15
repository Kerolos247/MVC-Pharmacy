using WebApplication4.Application.Common.Results;
using WebApplication4.Application.Dto.Prescription;
using WebApplication4.Application.IServices;
using WebApplication4.Domain.Models;

namespace WebApplication4.Domain.IRepository
{
    public interface IPrescriptionRepo
    {
        Task<Prescription?> GetByIdAsync(int id);
        Task<List<Prescription>> GetAllPrescriptionsAsync();
        Task<Result<bool>> CreateAsync(RequestCreatePrescription dto);
        Task<Result<bool>> UpdateAsync(int id, UpdatePrescriptionDto dto, string pharmacistId);
        Task<Result<bool>> DeleteAsync(int id);

        Task<ResponseCostDto> PayAsync(int id, IPayment payment);

    }
}
