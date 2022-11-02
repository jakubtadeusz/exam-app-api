using ExamApp.Domain.Enums;

namespace ExamApp.Domain.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Content { get; set; } = string.Empty;
        public QuestionType Type { get; set; } = QuestionType.SingleChoice;
        public int Points { get; set; } = 1;
        public int ExamId { get; set; }
        public virtual Exam? Exam { get; set; }
        public virtual ICollection<Answer>? Answers { get; set; }
    }
}