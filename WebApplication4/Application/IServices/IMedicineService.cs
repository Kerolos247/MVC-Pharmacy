using WebApplication4.Application.Common.Results;
using WebApplication4.Application.Dto.Medcine;
using WebApplication4.Domain.Models;

namespace WebApplication4.Application.IServices
{
    public interface IMedicineService
    {
        Task<List<Medicine>> GetAllMedicinesAsync();
        Task<Medicine?> GetByIdAsync(int id);
        Task<Result<bool>> CreateAsync(RequestCreateMedcine medicine);
        Task<Result<bool>> UpdateAsync(int id, UpdateMedcineDto medicine);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
