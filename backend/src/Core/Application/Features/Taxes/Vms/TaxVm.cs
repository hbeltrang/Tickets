using Tickets.Domain.Common;

namespace Tickets.Application.Features.Taxes.Vms
{
    public class TaxVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Percent { get; set; }
        public bool IsDefault { get; set; }
        public Status? Status { get; set; }
    }
}
