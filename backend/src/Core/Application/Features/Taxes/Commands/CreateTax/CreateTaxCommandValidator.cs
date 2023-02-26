using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.Taxes.Commands.CreateTax
{
    public class CreateTaxCommandValidator : AbstractValidator<CreateTaxCommand>
    {
        public CreateTaxCommandValidator()
        {
            RuleFor(p => p.Name)
               .NotNull().WithMessage(MessageLabel.ValidatorNameEmpty);

            RuleFor(p => p.Percent)
               .NotNull().WithMessage(MessageLabel.ValidatorPercentEmpty)
               .LessThanOrEqualTo(100)
               .PrecisionScale(10, 2, true);

        }
    }
}
