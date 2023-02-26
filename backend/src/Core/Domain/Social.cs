using Tickets.Domain.Common;

namespace Tickets.Domain
{
    public class Social : BaseDomainModel
    {
        public string? Name { get; set; }
        public string? PageUrl { get; set; }
        public virtual ICollection<SocialImage>? SocialImages { get; set; }
    }
}
