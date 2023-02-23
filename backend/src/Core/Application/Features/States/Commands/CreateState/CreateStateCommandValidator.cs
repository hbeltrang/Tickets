using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.States.Commands.CreateState
{
    public class CreateStateCommandValidator : AbstractValidator<CreateStateCommand>
    {
        public CreateStateCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(MessageLabel.ValidatorNameEmpty);

            //.MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres");

            RuleFor(p => p.CountryId)
                    .NotEmpty().WithMessage(MessageLabel.ValidatorCountryEmpty);
        }

    }
}
