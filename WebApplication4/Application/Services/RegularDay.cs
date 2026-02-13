using WebApplication4.Application.IServices;
using WebApplication4.Domain.Models;
namespace WebApplication4.Application.Services
{
    public class RegularDay : IPayment
    {
        public decimal CalculateCost(List<Medicine> medicines)
        {
            return medicines.Sum(m => m.Price);
        }
    }

}
