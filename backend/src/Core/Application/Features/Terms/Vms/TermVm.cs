using Tickets.Domain.Common;

namespace Tickets.Application.Features.Terms.Vms
{
    public class TermVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Content { get; set; }
        public Status? Status { get; set; }
    }
}
