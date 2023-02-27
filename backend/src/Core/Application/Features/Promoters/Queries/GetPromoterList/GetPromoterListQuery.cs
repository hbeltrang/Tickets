using MediatR;
using Tickets.Application.Features.Promoters.Vms;

namespace Tickets.Application.Features.Promoters.Queries.GetPromoterList
{
    public class GetPromoterListQuery : IRequest<IReadOnlyList<PromoterVm>>
    {
    }
}
