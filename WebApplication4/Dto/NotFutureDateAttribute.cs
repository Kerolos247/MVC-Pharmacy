using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Dto
{
    public class NotFutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime dateValue)
            {
                // التاريخ لازم يكون أقل من أو يساوي اليوم
                return dateValue.Date <= DateTime.Today;
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} cannot be in the future.";
        }
    }
}
