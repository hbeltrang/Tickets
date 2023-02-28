using System.Runtime.Serialization;

namespace Tickets.Domain.Common
{
    public enum Status
    {
        [EnumMember(Value = "0")]
        Inactive,
        [EnumMember(Value = "1")]
        Active
    }
}
