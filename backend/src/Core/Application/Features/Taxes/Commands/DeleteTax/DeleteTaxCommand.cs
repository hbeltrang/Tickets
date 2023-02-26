using MediatR;
using Tickets.Application.Features.Taxes.Vms;

namespace Tickets.Application.Features.Taxes.Commands.DeleteTax
{
    public class DeleteTaxCommand : IRequest<TaxVm>
    {
        public int TaxId { get; set; }

        public DeleteTaxCommand(int taxId)
        {
            TaxId = taxId == 0 ? throw new ArgumentException(nameof(taxId)) : taxId;
        }
    }
}
