using ExamApp.Intrastructure.Context;
using ExamApp.Intrastructure.Repository.Implementations;
using ExamApp.Intrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Intrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ExamDb");
            services.AddDbContext<ExamDbContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });

            services.AddScoped<IExamRepository, ExamRepository>();
            services.AddScoped<IExaminedRepository, ExaminedRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();            

            return services;
        }
    }
}
