using ExamApp.Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ExamApp.Domain.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime ExamTime { get; set; }

        [JsonProperty("examDuration")]
        public int Duration { get; set; }
        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ExamStatus ExamStatus { get; set; } = ExamStatus.NotStarted;
        public Guid OwnerId { get; set; }

        public virtual ICollection<Question>? Questions { get; set; }
        public virtual ICollection<Result>? Answers { get; set; }
    }
}
