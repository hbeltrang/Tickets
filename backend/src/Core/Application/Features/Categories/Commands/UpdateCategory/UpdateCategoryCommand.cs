using MediatR;
using Tickets.Application.Features.Categories.Vms;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<CategoryVm>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Status? Status { get; set; }
    }
}
