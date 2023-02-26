using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.Terms.Commands.UpdateTerm
{
    public class UpdateTermCommandValidator : AbstractValidator<UpdateTermCommand>
    {
        public UpdateTermCommandValidator()
        {
            RuleFor(p => p.Name)
                   .NotEmpty().WithMessage(MessageLabel.ValidatorNameEmpty);

        }
    }
}
