using Tickets.Domain.Common;

namespace Tickets.Domain
{
    public class City: BaseDomainModel
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int CountryId { get; set; }
        public Country? Country { get; set; }
        public int StateId { get; set; }
        public State? State { get; set; }
    }
}
