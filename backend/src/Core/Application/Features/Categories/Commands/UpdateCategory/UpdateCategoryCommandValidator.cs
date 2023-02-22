using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(p => p.Name)
                   .NotEmpty().WithMessage(MessageLabel.ValidatorNameEmpty);
        }
    }
}
