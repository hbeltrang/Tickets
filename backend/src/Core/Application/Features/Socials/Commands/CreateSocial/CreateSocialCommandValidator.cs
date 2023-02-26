using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.Socials.Commands.CreateSocial
{
    public class CreateSocialCommandValidator : AbstractValidator<CreateSocialCommand>
    {
        public CreateSocialCommandValidator()
        {
            RuleFor(p => p.Name)
               .NotNull().WithMessage(MessageLabel.ValidatorNameEmpty);

        }
    }
}
