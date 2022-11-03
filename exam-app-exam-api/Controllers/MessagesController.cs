using exam_app_exam_api_host.Utilities;
using ExamApp.Domain.Models;
using ExamApp.Intrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace exam_app_exam_api_host.Controllers
{
    public class MessagesController : BaseController
    {
        private readonly IMessageRepository _messageRepository;

        public MessagesController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages([FromQuery] int ownerId)
        {
            var messages = await _messageRepository.GetMessagesAsync(ownerId);
            var result = new ServiceResponse<IEnumerable<Message>>(System.Net.HttpStatusCode.OK)
            {
                ResponseContent = messages
            };

            return SendResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage([FromBody] Message message)
        {
            var result = new ServiceResponse<Message>(System.Net.HttpStatusCode.OK);
            var addedMessage = await _messageRepository.CreateMessageAsync(message);
            result.ResponseContent = addedMessage;

            return SendResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMessage([FromBody] Message message)
        {
            var result = new ServiceResponse<Message>(System.Net.HttpStatusCode.OK);
            var updatedMessage = await _messageRepository.UpdateMessageAsync(message);
            result.ResponseContent = updatedMessage;

            return SendResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage([FromRoute] int id)
        {
            var result = new ServiceResponse<Message>(System.Net.HttpStatusCode.OK);
            await _messageRepository.DeleteMessageAsync(id);

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

            return SendResponse(result);
        }
    }
}
