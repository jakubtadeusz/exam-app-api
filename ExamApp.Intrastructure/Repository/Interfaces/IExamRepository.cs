using ExamApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Intrastructure.Repository.Interfaces
{
    public interface IExamRepository
    {
        Task<IEnumerable<Exam>> GetExamsAsync(Guid ownerId);
        Task<Exam> GetExamByIdAsync(int id);
        Task<Exam> CreateExamAsync(Exam exam);
        Task<Exam> UpdateExamAsync(Exam exam);
        Task DeleteExamAsync(int id);
    }
}
