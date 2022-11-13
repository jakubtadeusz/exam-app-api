using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Intrastructure.ServiceBus
{
    public class ServiceBusMessager
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ServiceBusMessager> _logger;

        public ServiceBusMessager(IConfiguration configuration, ILogger<ServiceBusMessager> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendStartExamMessage(int examId)
        {
            ServiceBusClient client;
            ServiceBusSender sender;

            var clientOptions = new ServiceBusClientOptions
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };

            client = new ServiceBusClient(
                _configuration["ServiceBusConnectionString"],
                clientOptions);
            sender = client.CreateSender(_configuration["ServiceBusQueueName"]);

            var message = new ServiceBusMessage(examId.ToString());
            await sender.SendMessageAsync(message);
            _logger.LogInformation($"Exam start message sent for examId: {examId}");
        }
    }
}
