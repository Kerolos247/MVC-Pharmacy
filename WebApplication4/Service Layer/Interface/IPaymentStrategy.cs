using WebApplication4.Models;

namespace WebApplication4.Service_Layer.Interface
{
    public interface IPaymentStrategy
    {
        decimal CalculateCost(List<Medicine> medicines);
    }
}
