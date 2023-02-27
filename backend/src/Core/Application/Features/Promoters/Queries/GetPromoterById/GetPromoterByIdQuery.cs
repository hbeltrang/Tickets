using MediatR;
using Tickets.Application.Features.Promoters.Vms;

namespace Tickets.Application.Features.Promoters.Queries.GetPromoterById
{
    public class GetPromoterByIdQuery : IRequest<PromoterVm>
    {
        public int PromoterId { get; set; }

        public GetPromoterByIdQuery(int promoterId)
        {
            PromoterId = promoterId == 0 ? throw new ArgumentNullException(nameof(promoterId)) : promoterId;
        }

    }
}
