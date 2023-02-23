using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.Countries.Commands.UpdateCountry
{
    public class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
    {
        public UpdateCountryCommandValidator()
        {
            RuleFor(p => p.Name)
              .NotNull().WithMessage(MessageLabel.ValidatorNameEmpty);

            RuleFor(p => p.Code)
               .NotNull().WithMessage(MessageLabel.ValidatorAbbreviationEmpty);


        }
    }
}
