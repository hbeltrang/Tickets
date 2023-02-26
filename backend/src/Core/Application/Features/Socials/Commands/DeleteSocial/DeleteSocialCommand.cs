using MediatR;
using Tickets.Application.Features.Socials.Vms;

namespace Tickets.Application.Features.Socials.Commands.DeleteSocial
{
    public class DeleteSocialCommand : IRequest<SocialVm>
    {
        public int SocialId { get; set; }

        public DeleteSocialCommand(int socialId)
        {
            SocialId = socialId == 0 ? throw new ArgumentException(nameof(socialId)) : socialId;
        }

    }
}
