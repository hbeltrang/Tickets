using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.Promoters.Commands.UpdatePromoter
{
    public class UpdatePromoterCommandValidator : AbstractValidator<UpdatePromoterCommand>
    {
        public UpdatePromoterCommandValidator()
        {
            RuleFor(p => p.Name)
              .NotNull().WithMessage(MessageLabel.ValidatorNameEmpty);

            RuleFor(p => p.LastName)
              .NotNull().WithMessage(MessageLabel.ValidatorLastNameEmpty);

            RuleFor(p => p.Email)
              .NotNull().WithMessage(MessageLabel.ValidatorEmailEmpty);

            RuleFor(p => p.CellPhone)
              .NotNull().WithMessage(MessageLabel.ValidatorNameEmpty);

        }
    }
}
