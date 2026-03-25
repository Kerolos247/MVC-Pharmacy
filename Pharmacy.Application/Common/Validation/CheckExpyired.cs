using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Application.Common.Validation
{
    public class CheckExpyired : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime dateValue)
            {
                return dateValue.Date >= DateTime.Today;
            }
            return false;

        }

    }
}
