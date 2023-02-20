using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.Auths.Users.Commands.UpdateAdminUser
{
    public class UpdateAdminUserCommandValidator : AbstractValidator<UpdateAdminUserCommand>
    {
        public UpdateAdminUserCommandValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage(MessageLabel.ValidatorNameEmpty);

            RuleFor(x => x.LastName)
            .NotEmpty().WithMessage(MessageLabel.ValidatorLastNameEmpty);

            RuleFor(x => x.Phone)
            .NotEmpty().WithMessage(MessageLabel.ValidatorPhoneEmpty);
        }

    }
}
