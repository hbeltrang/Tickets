using System.Runtime.Serialization;

namespace Tickets.Domain.Common
{
    public enum Status
    {
        [EnumMember(Value = "false")]
        Inactive,
        [EnumMember(Value = "true")]
        Active
    }
}
