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
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ExamDbContext _context;

        public AnswerRepository(ExamDbContext context)
        {
            _context = context;
        }

        public async Task<Answer> CreateAnswerAsync(Answer answer)
        {
            await _context.Answers.AddAsync(answer);
            await _context.SaveChangesAsync();
            return answer;
        }

        public async Task DeleteAnswerAsync(int id)
        {
            var answer = await GetAnswerByIdAsync(id);
            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();
        }

        public async Task<Answer> GetAnswerByIdAsync(int id)
        {
            var answer = await _context.Answers.FindAsync(id);
            if (answer == null)
            {
                throw new KeyNotFoundException($"Answer doesnt exist id {id}");
            }

            return answer;
        }

        public async Task<IEnumerable<Answer>> GetAnswersAsync(int questionId)
        {
            var result = await _context.Answers.Where(x => x.QuestionId == questionId).ToListAsync();
            return result;
        }

        public async Task<Answer> UpdateAnswerAsync(Answer answer)
        {
            _context.Answers.Update(answer);
            await _context.SaveChangesAsync();
            return answer;
        }
    }
}
