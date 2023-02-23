using Tickets.Domain.Common;

namespace Tickets.Domain
{
    public class State: BaseDomainModel
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? slug { get; set; }
        public int CountryId { get; set; }
        public Country? Country { get; set; }

    }
}
