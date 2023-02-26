using MediatR;
using Tickets.Application.Features.Privacy.Vms;

namespace Tickets.Application.Features.Privacy.Queries.GetPrivacyList
{
    public class GetPrivacyListQuery : IRequest<IReadOnlyList<PrivacyVm>>
    {

    }
}
