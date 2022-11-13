using ExamApp.Domain.Models;
using ExamApp.Intrastructure.Context;
using ExamApp.Intrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Intrastructure.Repository.Implementations
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ExamDbContext _context;

        public QuestionRepository(ExamDbContext context)
        {
            _context = context;
        }

        public async Task<Question> AddQuestionAsync(Question question)
        {
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();

            return question;
        }

        public async Task DeleteQuestionAsync(int id)
        {
            var question = await _context.Questions.FindAsync(id);

            if (question == null)
            {
                return;
            }
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return;
        }

        public async Task<Question> GetQuestionByIdAsync(int id)
        {
            var question = await _context.Questions.FindAsync(id);

            if (question == null)
            {
                throw new KeyNotFoundException($"Question id {id} not found");
            }
            return question;
        }

        public async Task<IEnumerable<Question>> GetQuestionsAsync(int examId)
        {
            var questions = await _context.Questions.Include(x => x.Answers).Where(q => q.ExamId == examId).ToListAsync();

            return questions;
        }

        public async Task<Question> UpdateQuestionAsync(Question question)
        {
            _context.Entry(question).State = EntityState.Modified;
            foreach (var answer in question.Answers)
            {
                _context.Entry(answer).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();

            return question;
        }

        public async Task<List<Question>> UpdateQuestionsAsync(List<Question> questions)
        {
            var newQuestions = new List<Question>();
            foreach (var question in questions)
            {
                var q = await UpdateQuestionAsync(question);
                newQuestions.Add(q);
            }
            return newQuestions;
        }
    }
}
