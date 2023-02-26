using Tickets.Application.Features.SocialImages.Vms;
using Tickets.Domain.Common;

namespace Tickets.Application.Features.Socials.Vms
{
    public class SocialVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PageUrl { get; set; }
        public Status? Status { get; set; }
        public virtual ICollection<SocialImageVm>? SocialImages { get; set; }
    }
}
