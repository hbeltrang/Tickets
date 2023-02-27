using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.Companies.Commands.UpdateCompany
{
    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
    {
        public UpdateCompanyCommandValidator()
        {
            RuleFor(p => p.Name)
               .NotNull().WithMessage(MessageLabel.ValidatorNameEmpty);

        }
    }
}
