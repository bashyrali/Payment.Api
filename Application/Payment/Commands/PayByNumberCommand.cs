using Domain.Enums;
using MediatR;

namespace Application.Payment.Commands
{
    public class PayByNumberCommand : IRequest<string>
    {
        public string PhoneNumber { get; set; }
        public decimal Sum { get; set; }
    }
}