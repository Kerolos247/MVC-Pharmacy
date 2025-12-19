using WebApplication4.Models;
using WebApplication4.Service_Layer.Interface;

namespace WebApplication4.Service_Layer.Implementation
{
    public class Discussion_DR_Raghda:IPaymentStrategy
    {

        public decimal CalculateCost(List<Medicine> medicines)
        {
            decimal totalCost = medicines.Sum(m => m.Price);
            return totalCost * 0.5m; 

        }

    }
}
