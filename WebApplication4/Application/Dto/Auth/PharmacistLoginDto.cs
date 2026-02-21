using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Application.Dto.Auth
{
    public class PharmacistLoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
