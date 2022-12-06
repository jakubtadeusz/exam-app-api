using exam_app_exam_api_host.Utilities;
using ExamApp.Domain.Enums;
using ExamApp.Domain.Models;
using ExamApp.Intrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace exam_app_exam_api_host.Controllers
{
    public class MessagesController : BaseController
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(IMessageRepository messageRepository, ILogger<MessagesController> logger)
        {
            _messageRepository = messageRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages([FromQuery][Optional] string? type)
        {
            var ownerId = GetUserGuid(this.User);

            _logger.LogInformation($"User {ownerId} requested {type} messages");

            var messages = await _messageRepository.GetMessagesAsync(ownerId, type);

            _logger.LogInformation($"Retrieved {type} messages from ExamAppMessaging");

            var result = new ServiceResponse<IEnumerable<Message>>(System.Net.HttpStatusCode.OK)
            {
                ResponseContent = messages
            };

            return SendResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage([FromBody] Message message)
        {
            var ownerId = GetUserGuid(this.User);
            message.OwnerId = ownerId;

            _logger.LogInformation($"User {ownerId} added {Enum.GetName(typeof(MessageType), message.Type)} message");

            var result = new ServiceResponse<Message>(System.Net.HttpStatusCode.OK);
            var addedMessage = await _messageRepository.CreateMessageAsync(message);
            result.ResponseContent = addedMessage;

            _logger.LogInformation($"Message {addedMessage.Id} created by ExamAppMessaging");

            return SendResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMessage([FromBody] Message message)
        {
            var result = new ServiceResponse<Message>(System.Net.HttpStatusCode.OK);
            var updatedMessage = await _messageRepository.UpdateMessageAsync(message);
            result.ResponseContent = updatedMessage;

            _logger.LogInformation($"Message {updatedMessage.Id} updated");

            return SendResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage([FromRoute] int id)
        {
            var result = new ServiceResponse<Message>(System.Net.HttpStatusCode.OK);
            await _messageRepository.DeleteMessageAsync(id);

            _logger.LogInformation($"Message {id} removed");

            return SendResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessageById([FromRoute] int id)
        {
            var message = await _messageRepository.GetMessageByIdAsync(id);
            var result = new ServiceResponse<Message>(System.Net.HttpStatusCode.OK)
            {
                ResponseContent = message
            };

            _logger.LogInformation($"Message {id} requested");

            return SendResponse(result);
        }

        [HttpPost("{messageId}/SendGrades/{examId}/{group}")]
        public async Task<IActionResult> SendGrades(int messageId, int examId, string group)
        {
            {
                var ownerId = GetUserGuid(this.User);
                var result = new ServiceResponse<int>(System.Net.HttpStatusCode.OK);

                _logger.LogInformation($"User {ownerId} requested grades sending for message: {messageId}, exam: {examId}, group: {group}");

                var messagesAmount = await _messageRepository.SendGradesAsync(messageId, examId, group);
                result.ResponseContent = messagesAmount;

                _logger.LogInformation($"SendGradesCommand sent for message: {messageId}, exam: {examId}, group: {group}");

                return SendResponse(result);
            }
        }
        
        [HttpPost("{messageId}/SendInvitations/{examId}/{group}")]
        public async Task<IActionResult> SendInvitations(int messageId, int examId, string group)
        {
            {
                var ownerId = GetUserGuid(this.User);
                var result = new ServiceResponse<int>(System.Net.HttpStatusCode.OK);

                _logger.LogInformation($"User {ownerId} requested invitations sending for message: {messageId}, exam: {examId}, group: {group}");

                var messagesAmount = await _messageRepository.SendInvitationsAsync(ownerId, messageId, examId, group);
                result.ResponseContent = messagesAmount;

                _logger.LogInformation($"SendInvitationsCommand sent for message: {messageId}, exam: {examId}, group: {group}");

                return SendResponse(result);
            }
        }
    }
}
