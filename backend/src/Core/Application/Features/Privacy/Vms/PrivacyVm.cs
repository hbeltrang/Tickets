using Tickets.Domain.Common;

namespace Tickets.Application.Features.Privacy.Vms
{
    public class PrivacyVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Content { get; set; }
        public Status? Status { get; set; }
    }
}
