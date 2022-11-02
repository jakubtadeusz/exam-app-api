using ExamApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Intrastructure.Context
{
    public class ExamDbContext : DbContext
    {
        public ExamDbContext(DbContextOptions<ExamDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Answer> Answers { get; set; } = default!;
        public DbSet<Exam> Exams { get; set; } = default!;
        public DbSet<Message> Messages { get; set; } = default!;
        public DbSet<Question> Questions { get; set; } = default!;
        public DbSet<Result> Results { get; set; } = default!;
        public DbSet<Examined> Examined { get; set; } = default!;
    }
}
