using MediatR;
using Tickets.Application.Features.Promoters.Vms;

namespace Tickets.Application.Features.Promoters.Commands.DeletePromoter
{
    public class DeletePromoterCommand : IRequest<PromoterVm>
    {
        public int PromoterId { get; set; }

        public DeletePromoterCommand(int promoterId)
        {
            PromoterId = promoterId == 0 ? throw new ArgumentException(nameof(promoterId)) : promoterId;
        }

    }
}
