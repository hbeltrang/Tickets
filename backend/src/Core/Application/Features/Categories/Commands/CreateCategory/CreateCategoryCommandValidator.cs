using FluentValidation;
using Tickets.Application.Messages;

namespace Tickets.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(p => p.Name)
               .NotNull().WithMessage(MessageLabel.ValidatorNameEmpty);
        }        

    }
}
