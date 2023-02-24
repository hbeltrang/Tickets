using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.Cities.Commands.UpdateCity
{
    public class UpdateCityCommandValidator : AbstractValidator<UpdateCityCommand>
    {
        public UpdateCityCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(MessageLabel.ValidatorNameEmpty);

            RuleFor(p => p.CountryId)
                    .NotEmpty().WithMessage(MessageLabel.ValidatorCountryEmpty);

            RuleFor(p => p.StateId)
                    .NotEmpty().WithMessage(MessageLabel.ValidatorStateEmpty);
        }
    }
}
