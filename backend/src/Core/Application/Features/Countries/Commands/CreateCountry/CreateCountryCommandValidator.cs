﻿using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.Countries.Commands.CreateCountry
{
    public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
    {
        public CreateCountryCommandValidator()
        {
            RuleFor(p => p.Name)
               .NotNull().WithMessage(MessageLabel.ValidatorNameEmpty);

            RuleFor(p => p.Iso2)
               .NotNull().WithMessage(MessageLabel.ValidatorIso2Empty);

            RuleFor(p => p.Iso3)
               .NotNull().WithMessage(MessageLabel.ValidatorIso3Empty);
        }
    }
}