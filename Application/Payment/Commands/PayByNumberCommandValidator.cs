using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Payment.Commands
{
    public class PayByNumberCommandValidator : AbstractValidator<PayByNumberCommand>
    {
        private readonly IStringLocalizer<PayByNumberCommandValidator> _localizer;
        public PayByNumberCommandValidator(IStringLocalizer<PayByNumberCommandValidator> localizer)
        {
            _localizer = localizer;
            RuleFor(x => x.Sum).GreaterThan(0).WithMessage(_localizer["negativeNumberError"])
                .NotEmpty().WithMessage(_localizer["emptyValue"])
                .LessThanOrEqualTo(300_000).WithMessage(_localizer["maxSum"]);
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage(_localizer["emptyValue"])
                .Length(12).WithMessage(_localizer["numberValid"])
                .Must(str => str.StartsWith("+7")).WithMessage(_localizer["numberValid"]);
        }
    }
}