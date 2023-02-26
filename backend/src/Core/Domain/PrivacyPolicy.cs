using Tickets.Domain.Common;

namespace Tickets.Domain
{
    public class PrivacyPolicy : BaseDomainModel
    {
        public string? Name { get; set; }
        public string? Content { get; set; }
    }
}
