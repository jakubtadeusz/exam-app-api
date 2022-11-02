namespace ExamApp.Domain.Models
{
    public class Result
    {
        public int Id { get; set; }
        public string? Answer { get; set; }
        public int QuestionId { get; set; }
        public virtual Question? Question { get; set; }
        public int ExaminedId { get; set; }
        public virtual Examined? Examined { get; set; }
        public int? Score { get; set; } = null;
    }
}