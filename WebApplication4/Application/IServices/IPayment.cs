using WebApplication4.Domain.Models;

namespace WebApplication4.Application.IServices
{
    public interface IPayment
    {
        decimal CalculateCost(List<Medicine> medicines);
    }
}
