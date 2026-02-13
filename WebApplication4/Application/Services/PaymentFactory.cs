using WebApplication4.Application.IServices;

namespace WebApplication4.Application.Services
{
    public static class PaymentFactory
    {
        public static IPayment GetStrategy()
        {
            var today = DateTime.Today;


            if (today.Month == 10 && today.Day == 6 || today.Month == 2 && today.Day == 14)
                return new Holiday();


            if (today.Month == 1 && today.Day == 1)
                return new ApplyHighValueDiscount();


            return new RegularDay();
        }
    }
}
