using Tickets.Domain.Common;

namespace Tickets.Domain
{
    public class State: BaseDomainModel
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Slug { get; set; }
        public int CountryId { get; set; }
        public Country? Country { get; set; }

        public virtual ICollection<City>? Cities { get; set; }
    }
}
