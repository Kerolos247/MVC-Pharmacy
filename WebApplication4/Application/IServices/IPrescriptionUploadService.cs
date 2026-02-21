using WebApplication4.Application.Common.Results;
using WebApplication4.Application.Dto.PrescriptionUploadDto;
using WebApplication4.Domain.Models;

namespace WebApplication4.Application.IServices
{
    public interface IPrescriptionUploadService
    {
        Task<Result<bool>> UploadPrescriptionAsync(PrescriptionUploadDto dto);
        Task<Result<List<PrescriptionUpload>>> GetAllPrescriptionsAsync();
        Task<Result<bool>> DeletePrescriptionAsync(int id);

    }
}
