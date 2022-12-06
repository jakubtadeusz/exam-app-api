using ExamApp.Domain.Models;
using ExamApp.Intrastructure.Repository.Interfaces;
using ExamApp.Intrastructure.ServiceBus;
using ExamApp.Intrastructure.ServiceBus.Messages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Intrastructure.Repository.Implementations
{
    public class MessageRepository : IMessageRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly IResultRepository _resultRepository;
        private readonly IExaminedRepository _examinedRepository;
        private readonly IExamRepository _examRepository;
        private readonly ServiceBusMessager _serviceBusMessager;

        public MessageRepository(
            IConfiguration configuration,
            IResultRepository resultRepository,
            IExaminedRepository examinedRepository,
            IExamRepository examRepository,
            ServiceBusMessager serviceBusMessager)
        {
            _baseAddress = configuration["MessagingAPI"];
            _httpClient = new HttpClient();
            _resultRepository = resultRepository;
            _examinedRepository = examinedRepository;
            _examRepository = examRepository;
            _serviceBusMessager = serviceBusMessager;
        }

        public async Task<Message> CreateMessageAsync(Message message)
        {
            var content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseAddress}", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Message>(responseContent);

            return result;
        }

        public async Task DeleteMessageAsync(int id)
        {
            await _httpClient.DeleteAsync($"{_baseAddress}/{id}");
        }

        public async Task<Message> GetMessageByIdAsync(int id)
        {
            var response = _httpClient.GetAsync($"{_baseAddress}/{id}").Result;
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Message>(responseContent);

            return result;
        }
        
        public async Task<IEnumerable<Message>> GetMessagesAsync(Guid ownerId, string? type)
        {
            var query = $"{_baseAddress}/?ownerId={ownerId}";
            if (!string.IsNullOrEmpty(type))
            {
                query += $"&type={type}";
            }
            
            var response = await _httpClient.GetAsync(query);
            var json = response.Content.ReadAsStringAsync().Result;
            var messages = JsonConvert.DeserializeObject<IEnumerable<Message>>(json);
            return messages;
        }

        public async Task<int> SendGradesAsync(int messageId, int examId, string group)
        {
            var results = await _resultRepository.GetResultsWithExaminedAsync(examId);
            var grades = results.GroupBy(x => x.Examined)
                .Select(x => new Grade
                {
                    Email = x.Key.Email,
                    Points = x.Sum(y => y.Score)
                }).ToList();
            
            var command = new GradesMessageCommand()
            {
                MessageId = messageId,
                Grades = grades
            };

            await _serviceBusMessager.SendGradesMessageCommand(command);

            return command.Grades.Count;
        }

        public async Task<int> SendInvitationsAsync(Guid ownerId, int messageId, int examId, string group)
        {
            var examined = await _examinedRepository.GetExaminedAsync(ownerId, group);
            var exam = await _examRepository.GetExamByIdAsync(examId);
            var command = new InvitationMessageCommand()
            {
                MessageId = messageId,
                ExamId = examId,
                ExamName = exam.Name,
                ExamDate = exam.ExamTime,
                ExaminedInfo = examined.ToDictionary(k => k.Id, v => v.Email)
            };

            await _serviceBusMessager.SendInvitationMessageCommand(command);
            
            return command.ExaminedInfo.Keys.Count;
        }

        public async Task<Message> UpdateMessageAsync(Message message)
        {
            var content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"{_baseAddress}/", content);
            return message;
        }
    }
}
