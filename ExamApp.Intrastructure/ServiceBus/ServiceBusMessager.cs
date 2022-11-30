using Azure.Messaging.ServiceBus;
using ExamApp.Intrastructure.ServiceBus.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
            sender = client.CreateSender(_configuration["ServiceBusQueueNames:StartExam"]);

            var message = new ServiceBusMessage(examId.ToString());
            await sender.SendMessageAsync(message);
            _logger.LogInformation($"Exam start message sent for examId: {examId}");
        }
        
        public async Task SendPlanExamMessage(int examId)
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
            sender = client.CreateSender(_configuration["ServiceBusQueueNames:PlanExam"]);

            var message = new ServiceBusMessage(examId.ToString());
            await sender.SendMessageAsync(message);
            _logger.LogInformation($"Exam start message sent for examId: {examId}");
        }

        public async Task SendGradesMessageCommand(GradesMessageCommand gradesMessageCommand)
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
            sender = client.CreateSender(_configuration["ServiceBusQueueNames:GradeTopic"]);

            var message = new ServiceBusMessage(JsonConvert.SerializeObject(gradesMessageCommand)); ;
            await sender.SendMessageAsync(message);
            _logger.LogInformation($"Command for grade messages sent for message: {gradesMessageCommand.MessageId}");
        }
        
        public async Task SendInvitationMessageCommand(InvitationMessageCommand invitationMessageCommand)
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
            sender = client.CreateSender(_configuration["ServiceBusQueueNames:InvitationsTopic"]);

            var message = new ServiceBusMessage(JsonConvert.SerializeObject(invitationMessageCommand)); ;
            await sender.SendMessageAsync(message);
            _logger.LogInformation($"Command for grade messages sent for message: {invitationMessageCommand.MessageId}");
        }

        public async Task SendFinishGradingCommand(int examId)
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
            sender = client.CreateSender(_configuration["ServiceBusQueueNames:FinishGradingTopic"]);

            var message = new ServiceBusMessage(examId.ToString());
            await sender.SendMessageAsync(message);
            _logger.LogInformation($"Command for finish grading sent for examId: {examId}");
        }
    }
}
