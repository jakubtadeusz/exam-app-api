using ExamApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Domain.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime ExamTime { get; set; }
        public int Duration { get; set; }
        public ExamStatus ExamStatus { get; set; } = ExamStatus.NotStarted;
        public int OwnerId { get; set; }

        public virtual ICollection<Question>? Questions { get; set; }
        public virtual ICollection<Result>? Answers { get; set; }
    }
}
