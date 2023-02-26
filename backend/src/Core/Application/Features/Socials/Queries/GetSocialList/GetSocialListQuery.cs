using MediatR;
using Tickets.Application.Features.Socials.Vms;

namespace Tickets.Application.Features.Socials.Queries.GetSocialList
{
    public class GetSocialListQuery : IRequest<IReadOnlyList<SocialVm>>
    {
    }
}
