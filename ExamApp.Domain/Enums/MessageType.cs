using System.Runtime.Serialization;

namespace ExamApp.Domain.Enums
{
    public enum MessageType
    {
        [EnumMember(Value = "Zaproszenie")]
        Invite,
        [EnumMember(Value = "Ocena")]
        Grade
    }
}
