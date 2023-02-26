using MediatR;
using Tickets.Application.Features.Taxes.Vms;

namespace Tickets.Application.Features.Taxes.Querys.GetTaxById
{
    public class GetTaxByIdQuery : IRequest<TaxVm>
    {
        public int TaxId { get; set; }

        public GetTaxByIdQuery(int taxId)
        {
            TaxId = taxId == 0 ? throw new ArgumentNullException(nameof(taxId)) : taxId;
        }

    }
}
