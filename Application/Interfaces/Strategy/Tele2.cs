using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Strategy
{
    public class Tele2 : IPay
    {
        public void Pay(PaymentInfo payment)
        {
            payment.NumberType = NumberType.Tele2;
        }
    }
}