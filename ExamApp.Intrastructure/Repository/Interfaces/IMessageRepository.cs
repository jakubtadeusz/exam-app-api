using ExamApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Intrastructure.Repository.Interfaces
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetMessagesAsync(Guid ownerId, string? type);
        Task<Message> GetMessageByIdAsync(int id);
        Task<Message> CreateMessageAsync(Message message);
        Task<Message> UpdateMessageAsync(Message message);
        Task DeleteMessageAsync(int id);
        Task<int> SendGradesAsync(int examId, int examId1, string group);
        Task<int> SendInvitationsAsync(Guid ownerId, int examId, int examId1, string group);
    }
}
