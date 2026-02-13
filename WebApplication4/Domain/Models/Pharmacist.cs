using Microsoft.AspNetCore.Identity;

namespace WebApplication4.Domain.Models
{

    public class Pharmacist : IdentityUser
    {
        public string? FullName { get; set; }

        // Relations
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
