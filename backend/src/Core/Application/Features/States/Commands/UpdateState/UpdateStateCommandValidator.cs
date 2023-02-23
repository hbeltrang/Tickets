using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.States.Commands.UpdateState
{
    public class UpdateStateCommandValidator : AbstractValidator<UpdateStateCommand>
    {
        public UpdateStateCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(MessageLabel.ValidatorNameEmpty);

            RuleFor(p => p.CountryId)
                    .NotEmpty().WithMessage(MessageLabel.ValidatorCountryEmpty);
        }
    
    }
}
