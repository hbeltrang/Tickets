using MediatR;
using Tickets.Application.Features.Socials.Vms;

namespace Tickets.Application.Features.Socials.Queries.GetSocialByid
{
    public class GetSocialByIdQuery : IRequest<SocialVm>
    {
        public int SocialId { get; set; }

        public GetSocialByIdQuery(int socialId)
        {
            SocialId = socialId == 0 ? throw new ArgumentNullException(nameof(socialId)) : socialId;
        }

    }

}
