using Tickets.Domain.Common;

namespace Tickets.Domain
{
    public class Term : BaseDomainModel
    {
        public string? Name { get; set; }
        public string? Content { get; set; }
    }
}
