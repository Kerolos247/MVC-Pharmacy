using Microsoft.EntityFrameworkCore;
using WebApplication4.Application.IServices;
using WebApplication4.Infrastructure.DB;

namespace WebApplication4.Application.Services
{
    public class ValidationService : IValidationService
    {
        private readonly ApplicationDbContext _context;

        public ValidationService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Checks if a phone exists in the database for any entity T.
        /// Optional: exclude a specific entity by its Id.
        /// idPropertyName should match the primary key property of the entity (e.g., "PatientId", "SupplierId").
        /// </summary>
        public async Task<bool> PhoneExistsAsync<T>(string phone, string idPropertyName, int? excludeId = null) where T : class
        {
            var dbSet = _context.Set<T>();
            return await dbSet.AnyAsync(e =>
                EF.Property<string>(e, "Phone") == phone &&
                (!excludeId.HasValue || EF.Property<int>(e, idPropertyName) != excludeId.Value));
        }

        /// <summary>
        /// Checks if an email exists in the database for any entity T.
        /// Optional: exclude a specific entity by its Id.
        /// idPropertyName should match the primary key property of the entity (e.g., "PatientId", "SupplierId").
        /// </summary>
        public async Task<bool> EmailExistsAsync<T>(string email, string idPropertyName, int? excludeId = null) where T : class
        {
            var dbSet = _context.Set<T>();
            return await dbSet.AnyAsync(e =>
                EF.Property<string>(e, "Email") == email &&
                (!excludeId.HasValue || EF.Property<int>(e, idPropertyName) != excludeId.Value));
        }
    }
}
