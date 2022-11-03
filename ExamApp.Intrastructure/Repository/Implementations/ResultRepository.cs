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

        public async Task<Result> CreateResultAsync(Result result)
        {
            await _context.Results.AddAsync(result);
            await _context.SaveChangesAsync();

            return result;
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
            var results = await _context.Results.Include(x => x.Question).Where(r => r.Question.ExamId == examId).ToListAsync();

            return results;
        }

        public async Task<Result> UpdateResultAsync(Result result)
        {
            _context.Results.Update(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
