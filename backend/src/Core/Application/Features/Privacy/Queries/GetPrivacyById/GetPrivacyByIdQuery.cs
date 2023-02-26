using MediatR;
using Tickets.Application.Features.Privacy.Vms;

namespace Tickets.Application.Features.Privacy.Queries.GetPrivacyById
{
    public class GetPrivacyByIdQuery : IRequest<PrivacyVm>
    {
        public int PrivacyId { get; set; }

        public GetPrivacyByIdQuery(int privacyId)
        {
            PrivacyId = privacyId == 0 ? throw new ArgumentNullException(nameof(privacyId)) : privacyId;
        }

    }

}
