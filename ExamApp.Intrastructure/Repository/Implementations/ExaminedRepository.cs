using ExamApp.Domain.Models;
using ExamApp.Intrastructure.Context;
using ExamApp.Intrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Intrastructure.Repository.Implementations
{
    public class ExaminedRepository : IExaminedRepository
    {
        private readonly ExamDbContext _context;

        public ExaminedRepository(ExamDbContext context)
        {
            _context = context;
        }

        public async Task<Examined> CreateExaminedAsync(Examined examined)
        {
            await _context.Examined.AddAsync(examined);
            await _context.SaveChangesAsync();
            return examined;
        }

        public async Task DeleteExaminedAsync(int id)
        {
            var examined = await GetExaminedByIdAsync(id);
            _context.Examined.Remove(examined);
            await _context.SaveChangesAsync();
        }

        public async Task<Examined> GetExaminedByIdAsync(int id)
        {
            var examined = await _context.Examined.FindAsync(id);
            if (examined == null)
            {
                throw new KeyNotFoundException($"Examined doesnt exist id {id}");
            }

            return examined;
        }

        public async Task<IEnumerable<Examined>> GetExaminedsAsync()
        {
            var result = await _context.Examined.ToListAsync();
            return result;
        }

        public async Task<Examined> UpdateExaminedAsync(Examined examined)
        {
            _context.Examined.Update(examined);
            await _context.SaveChangesAsync();
            return examined;
        }
    }
}
