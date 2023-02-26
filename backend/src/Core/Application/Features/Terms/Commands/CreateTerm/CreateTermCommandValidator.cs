using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.Terms.Commands.CreateTerm
{
    public class CreateTermCommandValidator : AbstractValidator<CreateTermCommand>
    {
        public CreateTermCommandValidator()
        {
            RuleFor(p => p.Name)
               .NotNull().WithMessage(MessageLabel.ValidatorNameEmpty);

        }
    }
}
