using MediatR;
using Tickets.Application.Features.Categories.Vms;

namespace Tickets.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<CategoryVm>
    {
        public int CategoryId { get; set; }

        public GetCategoryByIdQuery(int categoryId)
        {
            CategoryId = categoryId == 0 ? throw new ArgumentNullException(nameof(categoryId)) : categoryId;
        }
    }
}
