using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.Cities.Commands.CreateCity
{
    public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
    {
        public CreateCityCommandValidator() 
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(MessageLabel.ValidatorNameEmpty);

            RuleFor(p => p.StateId)
                    .NotEmpty().WithMessage(MessageLabel.ValidatorStateEmpty);

        }
    }
}
