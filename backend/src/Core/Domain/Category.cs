using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using Tickets.Domain.Common;

namespace Tickets.Domain
{
    public class Category : BaseDomainModel
    {

        [Column(TypeName = "nvarchar(100)")]
        public string? Name { get; set; }

    }
}
