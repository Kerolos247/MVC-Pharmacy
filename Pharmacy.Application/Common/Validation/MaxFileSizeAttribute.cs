using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Application.Common.Validation
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly long _maxBytes;
        public MaxFileSizeAttribute(long maxBytes)
        {
            _maxBytes = maxBytes;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > _maxBytes)
                    return new ValidationResult($"File size cannot exceed {_maxBytes / (1024 * 1024)} MB.");
            }
            return ValidationResult.Success;
        }
    }
}
