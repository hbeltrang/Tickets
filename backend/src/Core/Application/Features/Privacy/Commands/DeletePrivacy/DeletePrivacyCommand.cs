using MediatR;
using Tickets.Application.Features.Privacy.Vms;

namespace Tickets.Application.Features.Privacy.Commands.DeletePrivacy
{
    public class DeletePrivacyCommand : IRequest<PrivacyVm>
    {
        public int PrivacyId { get; set; }

        public DeletePrivacyCommand(int privacyId)
        {
            PrivacyId = privacyId == 0 ? throw new ArgumentException(nameof(privacyId)) : privacyId;
        }
    }
}
