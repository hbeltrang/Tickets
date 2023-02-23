using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.Countries.Commands.CreateCountry
{
    public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
    {
        public CreateCountryCommandValidator()
        {
            RuleFor(p => p.Name)
               .NotNull().WithMessage(MessageLabel.ValidatorNameEmpty);

            RuleFor(p => p.Code)
               .NotNull().WithMessage(MessageLabel.ValidatorAbbreviationEmpty);

        }
    }
}
