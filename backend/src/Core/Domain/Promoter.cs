using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tickets.Domain.Common;

namespace Tickets.Domain
{
    public class Promoter : BaseDomainModel
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? CellPhone { get; set; }
        public string? Address { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal FeeAmount { get; set; } = decimal.Zero;

        [Column(TypeName = "decimal(10,2)")]
        public decimal FeePercent { get; set; } = decimal.Zero;

        public string? NotifyEmail { get; set; }
        public string? NotifyEmailBcc { get; set; }
        public string? NotifyCellPhone { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return (Name + " " + LastName);
            }
        }

    }
}
