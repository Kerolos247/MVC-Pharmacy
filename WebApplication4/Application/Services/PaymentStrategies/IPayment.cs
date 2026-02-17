using WebApplication4.Domain.Models;

namespace WebApplication4.Application.Services.PaymentStrategies
{
    public interface IPayment
    {
        decimal CalculateCost(List<Medicine> medicines);
    }
}
