using WebApplication4.Domain.Models;

namespace WebApplication4.Application.Services.PaymentStrategies
{
    public class October6Discount : IPayment
    {
        public decimal CalculateCost(ICollection<Medicine> medicines)
        {
            return medicines.Sum(m => m.Price) * 0.9m; // 10% discount
        }
    }

}
