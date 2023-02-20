using Tickets.Domain.Common;

namespace Tickets.Domain
{
    public class Country : BaseDomainModel
    {
        public string? Name { get; set; }

        public string? Iso2 { get; set; }

        public string? Iso3 { get; set; }

    }
}
