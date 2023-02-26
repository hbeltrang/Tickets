using MediatR;
using Tickets.Application.Features.Taxes.Vms;

namespace Tickets.Application.Features.Taxes.Commands.CreateTax
{
    public class CreateTaxCommand : IRequest<TaxVm>
    {
        public string? Name { get; set; }
        public decimal? Percent { get; set; }
        public bool? IsDefault { get; set; }
    }
}
