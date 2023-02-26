using MediatR;
using Tickets.Application.Features.Taxes.Vms;

namespace Tickets.Application.Features.Taxes.Querys.GetTaxList
{
    public class GetTaxListQuery : IRequest<IReadOnlyList<TaxVm>>
    {
    }
}
