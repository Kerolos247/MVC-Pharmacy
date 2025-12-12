using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.DB
{
    public class ApplicationDbContext : IdentityDbContext<Pharmacist>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionItem> PrescriptionItems { get; set; }
        public DbSet<SupplierOrder> SupplierOrders { get; set; }
        public DbSet<SupplierOrderItem> SupplierOrderItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Medicine>()
                .HasOne(m => m.Inventory)
                .WithOne(i => i.Medicine)
                .HasForeignKey<Inventory>(i => i.MedicineId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Category>()
                .HasMany(c => c.Medicines)
                .WithOne(m => m.Category)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

           
            modelBuilder.Entity<Supplier>()
                .HasMany(s => s.Medicines)
                .WithOne(m => m.Supplier)
                .HasForeignKey(m => m.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

           
            modelBuilder.Entity<Supplier>()
                .HasMany(s => s.Medicines)
                .WithOne(o => o.Supplier)
                .HasForeignKey(o => o.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

           
            modelBuilder.Entity<SupplierOrder>()
                .HasMany(o => o.Items)
                .WithOne(i => i.SupplierOrder)
                .HasForeignKey(i => i.SupplierOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            
            modelBuilder.Entity<Prescription>()
                .HasMany(p => p.PrescriptionItems)
                .WithOne(i => i.Prescription)
                .HasForeignKey(i => i.PrescriptionId)
                .OnDelete(DeleteBehavior.Restrict);

           
            modelBuilder.Entity<Medicine>()
                .HasMany(m => m.PrescriptionItems)
                .WithOne(i => i.Medicine)
                .HasForeignKey(i => i.MedicineId)
                .OnDelete(DeleteBehavior.Restrict);
        }



    }
}
