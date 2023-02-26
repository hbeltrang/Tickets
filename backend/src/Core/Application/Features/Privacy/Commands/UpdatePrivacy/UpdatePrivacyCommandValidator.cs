using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.Privacy.Commands.UpdatePrivacy
{
    public class UpdatePrivacyCommandValidator : AbstractValidator<UpdatePrivacyCommand>
    {
        public UpdatePrivacyCommandValidator()
        {
            RuleFor(p => p.Name)
                   .NotEmpty().WithMessage(MessageLabel.ValidatorNameEmpty);

        }
    }
}
