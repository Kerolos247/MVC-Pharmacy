using WebApplication4.Models;
using WebApplication4.Service_Layer.Interface;
namespace WebApplication4.Service_Layer.Implementation
{
    public class RegularDayStrategy : IPaymentStrategy
    {
        public decimal CalculateCost(List<Medicine> medicines)
        {
            return medicines.Sum(m => m.Price);
        }
    }
   
}
