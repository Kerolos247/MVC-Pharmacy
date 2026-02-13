using WebApplication4.Application.Dto;
using WebApplication4.Domain.IRepository;
using WebApplication4.Domain.Models;
using WebApplication4.Application.IServices;

namespace WebApplication4.Application.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepo _medicineRepo;

        public MedicineService(IMedicineRepo medicineRepo)
        {
            _medicineRepo = medicineRepo;
        }

      
        public async Task<List<Medicine>> GetAllMedicinesAsync()
        {
            return await _medicineRepo.GetAllMedicinesAsync();
        }

       
        public async Task<Medicine?> GetByIdAsync(int id)
        {
            return await _medicineRepo.GetByIdAsync(id);
        }

       
        public async Task<Medicine> CreateAsync(RequestCreateMedcine medicine)
        {
            return await _medicineRepo.CreateAsync(medicine);
        }

       
        public async Task<Medicine?> UpdateAsync(int id, UpdateMedcineDto medicine)
        {
            return await _medicineRepo.UpdateAsync(id, medicine);
        }

      
        public async Task<bool> DeleteAsync(int id)
        {
            return await _medicineRepo.DeleteAsync(id);
        }
    }
}
