using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Strategy
{
    public class Altel : IPay
    {
        public void Pay(PaymentInfo payment)
        {
            payment.NumberType = NumberType.Altel;
        }
    }
}