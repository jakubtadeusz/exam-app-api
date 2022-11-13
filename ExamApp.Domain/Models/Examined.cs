namespace ExamApp.Domain.Models
{
    public class Examined
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public Guid OwnerId { get; set; }
        public virtual ICollection<Result>? Results { get; set; }
    }
}