using Tickets.Domain.Common;

namespace Tickets.Application.Features.Countries.Vms
{
    public class CountryVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public Status? Status { get; set; }
    }
}
