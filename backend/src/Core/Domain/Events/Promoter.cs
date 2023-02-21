using System.ComponentModel.DataAnnotations.Schema;
using Tickets.Domain.Common;

namespace Tickets.Domain.Events
{
    public class Promoter : BaseDomainModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? CellPhone { get; set; }
        public string? Address { get; set; }

        [NotMapped]
        public string FullName {
            get { 
                return Name + " " + LastName;
            }
        }
    }
}
