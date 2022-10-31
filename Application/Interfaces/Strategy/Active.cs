using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Strategy
{
    public class Active : IPay
    {
        public void Pay(PaymentInfo payment)
        {
            payment.NumberType = NumberType.Active;
        }
    }
}