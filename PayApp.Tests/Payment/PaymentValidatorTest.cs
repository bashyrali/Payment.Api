using Application.Payment.Commands;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Xunit;

namespace PayApp.Tests.Payment
{
    public class PaymentValidatorTest
    {
        private readonly PayByNumberCommandValidator _validator;


        public PaymentValidatorTest()
        {
            var options = Options.Create(new LocalizationOptions {ResourcesPath = "Resources"});
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var localizer = new StringLocalizer<PayByNumberCommandValidator>(factory);
            _validator = new PayByNumberCommandValidator(localizer);
        }

        [Fact]
        public async void PaymentValidationSumError()
        {
            var command = new PayByNumberCommand {PhoneNumber = "+77001113322", Sum = -3100};

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(p => p.Sum);
        }

        [Fact]
        public async void PaymentValidationPhoneNumberError()
        {
            var command = new PayByNumberCommand {PhoneNumber = "+7700111332", Sum = 100};

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(p => p.PhoneNumber);
        }
    }
}