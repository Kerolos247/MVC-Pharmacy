using WebApplication4.Domain.IRepository;

namespace WebApplication4.Application.IServices
{
    public interface IUnitOfWork : IDisposable
    {
        IPrescriptionRepo Prescriptions { get; }
        IMedicineRepo Medicines { get; }
        IInventoryRepo Inventories { get; }
        ICategoryRepo Categories { get; }
        IPatientRepo patients { get; }
        ISupplierRepo suppliers { get; }

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
