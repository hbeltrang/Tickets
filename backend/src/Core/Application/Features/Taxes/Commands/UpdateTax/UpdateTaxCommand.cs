using MediatR;
using Tickets.Application.Features.Taxes.Vms;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Taxes.Commands.UpdateTax
{
    public class UpdateTaxCommand : IRequest<TaxVm>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Percent { get; set; }
        public bool? IsDefault { get; set; }
        public Status? Status { get; set; }
    }
}
