using ExamApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Intrastructure.Repository.Interfaces
{
    public interface IAnswerRepository
    {
        Task<IEnumerable<Answer>> GetAnswersAsync(int questionId);
        Task<Answer> GetAnswerByIdAsync(int id);
        Task<Answer> CreateAnswerAsync(Answer answer);
        Task<Answer> UpdateAnswerAsync(Answer answer);
        Task DeleteAnswerAsync(int id);
    }
}
