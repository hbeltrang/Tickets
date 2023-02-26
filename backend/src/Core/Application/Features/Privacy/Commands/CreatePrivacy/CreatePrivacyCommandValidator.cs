using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.Privacy.Commands.CreatePrivacy
{
    public class CreatePrivacyCommandValidator : AbstractValidator<CreatePrivacyCommand>
    {
        public CreatePrivacyCommandValidator()
        {
            RuleFor(p => p.Name)
               .NotNull().WithMessage(MessageLabel.ValidatorNameEmpty);

        }
    }
}
