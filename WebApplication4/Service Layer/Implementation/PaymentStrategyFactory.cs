using WebApplication4.Service_Layer.Interface;

namespace WebApplication4.Service_Layer.Implementation
{
    public static class PaymentStrategyFactory
    {
        public static IPaymentStrategy GetStrategy()
        {
            var today = DateTime.Today;

           
            if ((today.Month == 10 && today.Day == 6) || (today.Month == 2 && today.Day == 14))
                return new HolidayStrategy();


            if(today.Month==12&&today.Day==19)
                return new Discussion_DR_Raghda();

            return new RegularDayStrategy();
        }
    }
}
