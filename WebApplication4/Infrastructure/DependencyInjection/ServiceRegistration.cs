using WebApplication4.Application.Common.Validation;
using WebApplication4.Application.IServices;
using WebApplication4.Application.Services;
using WebApplication4.Domain.IRepository;
using WebApplication4.Infrastructure.Repository;
using WebApplication4.Infrastructure.Services;
using WebApplication4.Infrastructure.UintOfWork;

namespace WebApplication4.Infrastructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
           
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMedicineService, MedicineService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IPrescriptionService, PrescriptionService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IFileUploadService, CloudinaryFileUploadService>();
            services.AddScoped<IPrescriptionUploadService, PrescriptionUploadService>();
            services.AddScoped<IFeedBackService, FeedBackService>();
            services.AddScoped<ISentimentService, SentimentService>();



            services.AddScoped<IMedicineRepo, MedicineRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<ISupplierRepo, SupplierRepo>();
            services.AddScoped<IPatientRepo, PatientRepo>();
            services.AddScoped<IPrescriptionRepo, PrescriptionRepo>();
            services.AddScoped<IInventoryRepo, InventoryRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPrescriptionUploadRepo, PrescriptionUploadRepo>();
            services.AddScoped<IFeedBackRepo, FeedBackRepo>();




            services.AddMemoryCache();
           







        }
    }
}
