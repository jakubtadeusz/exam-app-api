using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Intrastructure.ServiceBus.Messages
{
    public class InvitationMessageCommand
    {
        public int MessageId { get; set; }
        public Dictionary<int, string> ExaminedInfo { get; set; }
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public DateTime ExamDate { get; set; }
    }
}
