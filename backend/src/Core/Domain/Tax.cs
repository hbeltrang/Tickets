using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using Tickets.Domain.Common;

namespace Tickets.Domain
{
    public class Tax : BaseDomainModel
    {
        public string? Name { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Percent { get; set; } = decimal.Zero;
        public bool IsDefault { get; set; } = false;
    }
}
