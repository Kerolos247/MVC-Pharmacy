using WebApplication4.Application.IServices;
using WebApplication4.Domain.Models;

namespace WebApplication4.Application.Services
{
    public class ApplyHighValueDiscount : IPayment
    {

        public decimal CalculateCost(List<Medicine> medicines)
        {
            decimal totalCost = medicines.Sum(m => m.Price);
            return totalCost * 0.3m;

        }

    }
}
