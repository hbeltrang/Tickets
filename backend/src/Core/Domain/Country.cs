using Tickets.Domain.Common;

namespace Tickets.Domain
{
    public class Country : BaseDomainModel
    {
        public string? Name { get; set; }

        public string? Code { get; set; }

        public virtual ICollection<State>? States { get; set; }
        //public virtual ICollection<City>? Cities { get; set; }
    }
}
