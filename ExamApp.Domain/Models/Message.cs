using ExamApp.Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Domain.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        [JsonConverter(typeof(StringEnumConverter))]
        public MessageType Type { get; set; }
        public Guid OwnerId { get; set; }
    }
}
