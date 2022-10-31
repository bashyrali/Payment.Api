using System;
using Application.Payment.Commands;
using Infrastructure;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace PayApp.Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly PaymentDbContext Context;
        protected readonly IStringLocalizer<PayByNumberCommandHandler> Localizer;


        protected TestCommandBase()
        {
            Context = PaymentContextFactory.Create();
            var options = Options.Create(new LocalizationOptions {ResourcesPath = "Resources"});
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            Localizer = new StringLocalizer<PayByNumberCommandHandler>(factory);
        }

        public void Dispose()
        {
            PaymentContextFactory.Destroy(Context);
        }
    }
}