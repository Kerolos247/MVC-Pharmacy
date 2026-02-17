namespace WebApplication4.Application.Common.Validation
{
    public interface IValidationService
    {
        /// <summary>
        /// Checks if a phone exists in the database for any entity T.
        /// Optional: exclude a specific entity by its Id.
        /// idPropertyName should match the primary key property of the entity (e.g., "PatientId", "SupplierId").
        /// </summary>
        Task<bool> PhoneExistsAsync<T>(string phone, string idPropertyName, int? excludeId = null) where T : class;

        /// <summary>
        /// Checks if an email exists in the database for any entity T.
        /// Optional: exclude a specific entity by its Id.
        /// idPropertyName should match the primary key property of the entity (e.g., "PatientId", "SupplierId").
        /// </summary>
        Task<bool> EmailExistsAsync<T>(string email, string idPropertyName, int? excludeId = null) where T : class;

    }
}
