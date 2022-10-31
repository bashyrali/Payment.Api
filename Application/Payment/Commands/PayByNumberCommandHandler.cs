using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Interfaces.Strategy;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;
using Serilog;

namespace Application.Payment.Commands
{
    public class PayByNumberCommandHandler : IRequestHandler<PayByNumberCommand, string>
    {
        private readonly IPaymentDbContext _ctx;
        private readonly IStringLocalizer<PayByNumberCommandHandler> _localizer;
        private IPay _pay;

        private readonly Dictionary<string, IPay> _operators = new()
        {
            {"701", new Active()},
            {"777", new Beeline()},
            {"705", new Beeline()},
            {"707", new Tele2()},
            {"747", new Tele2()},
            {"700", new Altel()},
            {"708", new Altel()},
        };

        public PayByNumberCommandHandler(IPaymentDbContext ctx, IStringLocalizer<PayByNumberCommandHandler> localizer)
        {
            _ctx = ctx;
            _localizer = localizer;
        }

        public async Task<string> Handle(PayByNumberCommand request, CancellationToken cancellationToken)
        {
            var key = request.PhoneNumber.Substring(2, 3);
            var payment = new PaymentInfo
            {
                PhoneNumber = request.PhoneNumber,
                Sum = request.Sum,
                Created = DateTime.Now
            };

            if (_operators.TryGetValue(key, out _pay))
                _pay.Pay(payment);
            else
            {
                Log.Error("Number operator {key} not found");
                throw new KeyNotFoundException(_localizer["NotFound"]); 
            }
                

            

            await _ctx.Payments.AddAsync(payment, cancellationToken);
            await _ctx.SaveChangesAsync(cancellationToken);
            Log.Information("Payment with Id:{id} was success",payment.Id);
            return payment.NumberType.ToString();
        }
    }
}