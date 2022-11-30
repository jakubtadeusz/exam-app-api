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
    public class ResultRepository : IResultRepository
    {
        private readonly ExamDbContext _context;

        public ResultRepository(ExamDbContext context)
        {
            _context = context;
        }

        public async Task<List<Result>> CreateResultsAsync(List<Result> results)
        {
            foreach (var result in results)
            {
                var oldResults = _context.Results.Where(x => x.ExaminedId == result.ExaminedId && x.QuestionId == result.QuestionId);
                _context.Results.RemoveRange(oldResults);
                result.ExamId = _context.Questions.Find(result.QuestionId).ExamId;
            }
            await _context.Results.AddRangeAsync(results);
            await _context.SaveChangesAsync();

            return results;
        }

        public async Task DeleteResultAsync(int id)
        {
            var result = await _context.Results.FindAsync(id);

            if (result == null)
            {
                return;
            }
            _context.Results.Remove(result);
            await _context.SaveChangesAsync();

            return;
        }

        public async Task<Result> GetResultByIdAsync(int id)
        {
            var result = await _context.Results.FindAsync(id);

            if (result == null)
            {
                throw new KeyNotFoundException($"Result id {id} not found");
            }
            return result;
        }

        public async Task<IEnumerable<Result>> GetResultsAsync(int examId)
        {
            var results = await _context.Results.Where(x => x.ExamId == examId).ToListAsync();

            return results;
        }

        public async Task<IEnumerable<Result>> GetResultsWithExaminedAsync(int examId)
        {
            var results = await _context.Results.Include(x => x.Examined).Where(x => x.ExamId == examId).ToListAsync();

            return results;
        }

        public async Task<List<Result>> GradeResultsAsync(List<Result> results)
        {
            foreach(var result in results)
            {
                var oldRes = _context.Results.Find(result.Id);
                if (oldRes != null)
                {
                    oldRes.Score = result.Score;
                }
            }

            await _context.SaveChangesAsync();
            return results;
        }
    }
}
