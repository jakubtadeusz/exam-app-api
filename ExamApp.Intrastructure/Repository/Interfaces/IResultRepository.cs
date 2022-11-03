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
        Task<Result> GetResultByIdAsync(int id);
        Task<Result> CreateResultAsync(Result result);
        Task<Result> UpdateResultAsync(Result result);
        Task DeleteResultAsync(int id);
    }
}
