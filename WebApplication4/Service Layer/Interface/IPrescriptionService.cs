using WebApplication4.Dto;
using WebApplication4.Models;

namespace WebApplication4.Service_Layer.Interface
{
    public interface IPrescriptionService
    {
        Task<Prescription?> GetByIdAsync(int id);
        Task<List<Prescription>> GetAllPrescriptionsAsync();
        Task<Prescription> CreateAsync(RequestCreatePrescription dto);
        Task<Prescription?> UpdateAsync(int id, UpdatePrescriptionDto dto);
        Task<bool> DeleteAsync(int id);

        Task<ResponseCostDto> PayAsync(int id,IPaymentStrategy payment);

    }
}  
