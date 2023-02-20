using MediatR;
using Tickets.Application.Features.Categories.Vms;

namespace Tickets.Application.Features.Categories.Queries.GetCategoryList
{
    public class GetCategoryListQuery : IRequest<IReadOnlyList<CategoryVm>>
    {

    }
}
