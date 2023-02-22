using MediatR;
using Tickets.Application.Features.Categories.Vms;

namespace Tickets.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<CategoryVm>
    {
        public int CategoryId { get; set; }

        public DeleteCategoryCommand(int categoryId)
        {
            CategoryId = categoryId == 0 ? throw new ArgumentException(nameof(categoryId)) : categoryId;
        }
    }
}
