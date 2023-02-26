using Tickets.Domain.Common;

namespace Tickets.Domain
{
    public class SocialImage: BaseDomainModel
    {
        public string? ImageUrl { get; set; }
        public int SocialId { get; set; }        
        public string? PublicCode { get; set; }
        public virtual Social? Social { get; set; }
    }
}
