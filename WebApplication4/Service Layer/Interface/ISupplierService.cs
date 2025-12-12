using WebApplication4.Dto;
using WebApplication4.Models;

namespace WebApplication4.Service_Layer.Interface
{
    public interface ISupplierService
    {
        Task<Supplier?> GetByIdAsync(int id);

        // Get all suppliers
        Task<List<Supplier>> GetAllSuppliersAsync();

        // Create a new supplier and return the created entity
        Task<Supplier> CreateAsync(RequestCreateSupplier dto);

        // Update supplier and return updated entity, or null if not found
        Task<Supplier?> UpdateAsync(int id, UpdateSupplierDto dto);

        // Delete supplier by Id, returns true if deleted, false if not found
        Task<bool> DeleteAsync(int id);
    }
}
