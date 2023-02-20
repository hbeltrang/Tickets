using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.Auths.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(MessageLabel.ValidatorNameEmpty);

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage(MessageLabel.ValidatorLastNameEmpty);

        }
    }
}
