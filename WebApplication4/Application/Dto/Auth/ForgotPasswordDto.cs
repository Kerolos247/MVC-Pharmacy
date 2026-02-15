using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Application.Dto.Auth
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
