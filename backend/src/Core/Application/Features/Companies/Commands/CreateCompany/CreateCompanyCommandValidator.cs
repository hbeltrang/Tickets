using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyCommandValidator()
        {
            RuleFor(p => p.Name)
               .NotNull().WithMessage(MessageLabel.ValidatorNameEmpty);


        }
    }
}
