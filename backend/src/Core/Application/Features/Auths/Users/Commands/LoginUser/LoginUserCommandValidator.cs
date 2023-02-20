using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.Auths.Users.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage(MessageLabel.ValidatorEmailEmpty);

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage(MessageLabel.ValidatorPasswordEmpty);
        }
    }
}
