using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Domain.Enums
{
    public enum ExamStatus
    {
        [EnumMember(Value = "Nierozpoczęty")]
        NotStarted,
        [EnumMember(Value = "W trakcie")]
        InProgress,
        [EnumMember(Value = "Do oceny")]
        ToGrade,
        [EnumMember(Value = "Zakończony")]
        Finished
    }
}
