using ExamApp.Intrastructure.Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Intrastructure.ServiceBus.Messages
{
    public class GradesMessageCommand
    {
        public int MessageId { get; set; }
        public List<Grade> Grades { get; set; } = new List<Grade>();
    }
}
