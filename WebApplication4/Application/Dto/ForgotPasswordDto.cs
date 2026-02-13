using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Application.Dto
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
