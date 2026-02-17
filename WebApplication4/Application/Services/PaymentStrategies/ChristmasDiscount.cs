using WebApplication4.Domain.Models;

namespace WebApplication4.Application.Services.PaymentStrategies
{
    public class ChristmasDiscount : IPayment
    {

        public decimal CalculateCost(List<Medicine> medicines)
        {
            decimal totalCost = medicines.Sum(m => m.Price);
            return totalCost * 0.7m;// 30% discount for high value prescriptions

        }

    }
}
