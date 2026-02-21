using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Application.Common.Validation;
using WebApplication4.Application.IServices;
using WebApplication4.Application.Services;
using WebApplication4.Domain.IRepository;
using WebApplication4.Domain.Models;
using WebApplication4.Infrastructure.DB;
using WebApplication4.Infrastructure.DependencyInjection;
using WebApplication4.Infrastructure.Repository;
using WebApplication4.Infrastructure.Seed;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using WebApplication4.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// ================= Controllers & Razor =================
builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Insert(0, "/Pressention/Views/{1}/{0}.cshtml");
        options.ViewLocationFormats.Insert(1, "/Pressention/Views/Shared/{0}.cshtml");
        options.ViewLocationFormats.Insert(2, "/Views/{1}/{0}.cshtml");
        options.ViewLocationFormats.Insert(3, "/Views/Shared/{0}.cshtml");
    });

builder.Services.AddHttpClient();

// ================= DbContext =================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ================= Cloudinary =================
var cloudinarySettings = builder.Configuration.GetSection("CloudinarySettings");

var account = new CloudinaryDotNet.Account(
    cloudinarySettings["CloudName"],
    cloudinarySettings["ApiKey"],
    cloudinarySettings["ApiSecret"]
);

// استخدم CloudinaryDotNet.Cloudinary لتجنب التعارض مع namespace
var cloudinary = new CloudinaryDotNet.Cloudinary(account);

builder.Services.AddSingleton(cloudinary);

// ================= Identity =================
builder.Services.AddIdentity<Pharmacist, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
    options.Lockout.AllowedForNewUsers = true;

    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders()
.AddDefaultUI();

// ================= Infrastructure Services =================
builder.Services.AddInfrastructureServices();

var app = builder.Build();

// ================= Seed Roles =================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await IdentitySeeder.SeedAsync(services);
}

// ================= Middleware =================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// ================= Routing =================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();