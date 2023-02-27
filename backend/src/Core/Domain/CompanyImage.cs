using Tickets.Domain.Common;

namespace Tickets.Domain
{
    public class CompanyImage : BaseDomainModel
    {
        public string? ImageUrl { get; set; }        
        public string? PublicCode { get; set; }
        public int CompanyId { get; set; }
        public virtual Company? Company { get; set; }
    }
}
