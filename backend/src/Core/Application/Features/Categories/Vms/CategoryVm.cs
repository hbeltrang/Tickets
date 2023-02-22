using Tickets.Domain.Common;

namespace Tickets.Application.Features.Categories.Vms
{
    public class CategoryVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Status? Status { get; set; }
    }
}
