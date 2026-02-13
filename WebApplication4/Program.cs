using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Application.IServices;
using WebApplication4.Application.Services;
using WebApplication4.Domain.IRepository;
using WebApplication4.Domain.Models;
using WebApplication4.Infrastructure.DB;
using WebApplication4.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        
        options.ViewLocationFormats.Insert(0, "/Pressention/Views/{1}/{0}.cshtml");
        options.ViewLocationFormats.Insert(1, "/Pressention/Views/Shared/{0}.cshtml");

       
        options.ViewLocationFormats.Insert(2, "/Views/{1}/{0}.cshtml");
        options.ViewLocationFormats.Insert(3, "/Views/Shared/{0}.cshtml");
    });


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<Pharmacist, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();


// ===== Services =====
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMedicineService, MedicineService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// ===== Repositories =====
builder.Services.AddScoped<IMedicineRepo, MedicineRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<ISupplierRepo, SupplierRepo>();
builder.Services.AddScoped<IPatientRepo, PatientRepo>();
builder.Services.AddScoped<IPrescriptionRepo, PrescriptionRepo>();
builder.Services.AddScoped<IInventoryRepo, InventoryRepo>();


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = { "Pharmacist", "Admin" };

    foreach (var roleName in roles)
    {
        var exist = await roleManager.RoleExistsAsync(roleName);
        if (!exist)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
