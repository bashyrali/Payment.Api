using System;
using Domain.Enums;

namespace Domain.Entities
{
    public class PaymentInfo
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Sum { get; set; }
        public NumberType NumberType { get; set; }
        public DateTime Created { get; set; }
    }
}