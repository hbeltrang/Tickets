using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.Taxes.Commands.UpdateTax
{
    public class UpdateTaxCommandValidator : AbstractValidator<UpdateTaxCommand>
    {
        public UpdateTaxCommandValidator()
        {
            RuleFor(p => p.Name)
                   .NotEmpty().WithMessage(MessageLabel.ValidatorNameEmpty);

        }
    }
}
