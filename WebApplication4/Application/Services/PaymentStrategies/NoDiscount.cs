using WebApplication4.Domain.Models;
namespace WebApplication4.Application.Services.PaymentStrategies
{
    public class NoDiscount : IPayment
    {
        public decimal CalculateCost(List<Medicine> medicines)
        {
            return medicines.Sum(m => m.Price);
        }
    }

}
