using ExamApp.Intrastructure.Context;
using ExamApp.Intrastructure.Repository.Implementations;
using ExamApp.Intrastructure.Repository.Interfaces;
using ExamApp.Intrastructure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IResultRepository, ResultRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<ServiceBusMessager>();

            return services;
        }
    }
}
