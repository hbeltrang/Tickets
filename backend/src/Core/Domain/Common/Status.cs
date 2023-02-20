using System.Runtime.Serialization;

namespace Tickets.Domain.Common
{
    public enum Status
    {
        [EnumMember(Value = "Inactive")]
        Inactive,
        [EnumMember(Value = "Active")]
        Active
    }
}
