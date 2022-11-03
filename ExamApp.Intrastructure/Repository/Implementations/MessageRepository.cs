using ExamApp.Domain.Models;
using ExamApp.Intrastructure.Context;
using ExamApp.Intrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Intrastructure.Repository.Implementations
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ExamDbContext _context;

        public MessageRepository(ExamDbContext context)
        {
            _context = context;
        }

        public async Task<Message> CreateMessageAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();

            return message;
        }

        public async Task DeleteMessageAsync(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return;
            }
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return;
        }

        public async Task<Message> GetMessageByIdAsync(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                throw new KeyNotFoundException($"Message id {id} not found");
            }
            return message;
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync(int ownerId)
        {
            var messages = await _context.Messages.Where(m => m.OwnerId == ownerId).ToListAsync();

            return messages;
        }

        public async Task<Message> UpdateMessageAsync(Message message)
        {
            _context.Messages.Update(message);
            await _context.SaveChangesAsync();
            return message;
        }
    }
}
