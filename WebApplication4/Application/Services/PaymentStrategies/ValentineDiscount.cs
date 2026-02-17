using WebApplication4.Domain.Models;
namespace WebApplication4.Application.Services.PaymentStrategies
{
    public class ValentineDiscount : IPayment
    {

        public decimal CalculateCost(List<Medicine> medicines)
        {

            decimal totalCost = medicines.Sum(m => m.Price);
            return totalCost * 0.8m; // 20% discount
        }
    }
}
