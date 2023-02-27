using System.ComponentModel.DataAnnotations;
using Tickets.Domain.Common;

namespace Tickets.Domain
{
    public class Company : BaseDomainModel
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? CellPhone { get; set; }
        public string? Address { get; set; }
        
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public string? MapUrl { get; set; }

        public virtual ICollection<CompanyImage>? CompanyImages { get; set; }

    }
}
