using Newtonsoft.Json;

namespace ExamApp.Domain.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int Order { get; set; }
        [JsonProperty("answer")]
        public string Content { get; set; } = string.Empty;
        [JsonProperty("correct")]
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}