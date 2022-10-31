using Domain.Entities;

namespace Application.Interfaces.Strategy
{
    public interface IPay
    {
        void Pay(PaymentInfo payment);
    }
}