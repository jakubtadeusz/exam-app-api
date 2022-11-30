using ExamApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Intrastructure.Repository.Interfaces
{
    public interface IResultRepository
    {
        Task<IEnumerable<Result>> GetResultsAsync(int examId);
        Task<IEnumerable<Result>> GetResultsWithExaminedAsync(int examId);
        Task<Result> GetResultByIdAsync(int id);
        Task<List<Result>> CreateResultsAsync(List<Result> result);
        Task<List<Result>> GradeResultsAsync(List<Result> result);
        Task DeleteResultAsync(int id);
    }
}
