using ExamApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Intrastructure.Repository.Interfaces
{
    public interface IExaminedRepository
    {
        Task<IEnumerable<Examined>> GetExaminedAsync(Guid ownerId);
        Task<IEnumerable<Examined>> GetExaminedAsync(Guid ownerId, string group);
        Task<Examined> GetExaminedByIdAsync(int id);
        Task<Examined> AddExaminedAsync(Examined examined);
        Task<Examined> UpdateExaminedAsync(Examined examined);
        Task DeleteExaminedAsync(int id);
    }
}
