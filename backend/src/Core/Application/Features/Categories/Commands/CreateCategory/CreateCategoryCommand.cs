using MediatR;
using Tickets.Application.Features.Categories.Vms;

namespace Tickets.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<CategoryVm>
    {
        public string? Name { get; set; }
    }
}
