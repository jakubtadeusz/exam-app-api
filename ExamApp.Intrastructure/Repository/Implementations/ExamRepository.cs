using ExamApp.Domain.Models;
using ExamApp.Intrastructure.Context;
using ExamApp.Intrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core;

namespace ExamApp.Intrastructure.Repository.Implementations
{
    public class ExamRepository : IExamRepository
    {
        private readonly ExamDbContext _context;

        public ExamRepository(ExamDbContext context)
        {
            _context = context;
        }

        public async Task<Exam> CreateExamAsync(Exam exam)
        {
            await _context.Exams.AddAsync(exam);
            await _context.SaveChangesAsync();
            return exam;
        }

        public async Task DeleteExamAsync(int id)
        {
            var exam = await GetExamByIdAsync(id);
            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();
        }

        public async Task<Exam> GetExamByIdAsync(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                throw new ObjectNotFoundException($"exam not found id = {id}");
            }
            return exam;
        }

        public async Task<IEnumerable<Exam>> GetExamsAsync(int ownerId)
        {
            var exams = await _context.Exams.Where(x => x.OwnerId == ownerId).ToListAsync();
            return exams;
        }

        public async Task<Exam> UpdateExamAsync(Exam exam)
        {
            _context.Exams.Update(exam);
            await _context.SaveChangesAsync();
            return exam;
        }
    }
}
