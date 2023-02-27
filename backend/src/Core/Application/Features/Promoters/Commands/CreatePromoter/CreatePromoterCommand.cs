using MediatR;
using Tickets.Application.Features.Promoters.Vms;

namespace Tickets.Application.Features.Promoters.Commands.CreatePromoter
{
    public class CreatePromoterCommand : IRequest<PromoterVm>
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? CellPhone { get; set; }
        public string? Address { get; set; }
        public decimal FeeAmount { get; set; } = decimal.Zero;
        public decimal FeePercent { get; set; } = decimal.Zero;
        public string? NotifyEmail { get; set; }
        public string? NotifyEmailBcc { get; set; }
        public string? NotifyCellPhone { get; set; }

    }
}
