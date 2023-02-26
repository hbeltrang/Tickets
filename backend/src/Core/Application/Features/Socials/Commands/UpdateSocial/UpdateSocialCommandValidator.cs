using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.Socials.Commands.UpdateSocial
{
    public class UpdateSocialCommandValidator : AbstractValidator<UpdateSocialCommand>
    {
        public UpdateSocialCommandValidator()
        {
            RuleFor(p => p.Name)
               .NotNull().WithMessage(MessageLabel.ValidatorNameEmpty);

        }
    }
}
