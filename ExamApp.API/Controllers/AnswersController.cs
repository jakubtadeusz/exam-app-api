using exam_app_exam_api_host.Utilities;
using ExamApp.Domain.Models;
using ExamApp.Intrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace exam_app_exam_api_host.Controllers
{
    public class AnswersController : BaseController
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly ILogger<AnswersController> _logger;

        public AnswersController(IAnswerRepository answerRepository, ILogger<AnswersController> logger)
        {
            _answerRepository = answerRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnswers([FromQuery] int questionId)
        {
            var answers = await _answerRepository.GetAnswersAsync(questionId);
            var result = new ServiceResponse<IEnumerable<Answer>>(System.Net.HttpStatusCode.OK)
            {
                ResponseContent = answers
            };

            _logger.LogInformation($"Requested answers for questionId: {questionId}");

            return SendResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnswer([FromBody] Answer answer)
        {
            var result = new ServiceResponse<Answer>(System.Net.HttpStatusCode.OK);
            var addedAnswer = await _answerRepository.CreateAnswerAsync(answer);
            result.ResponseContent = addedAnswer;

            _logger.LogInformation($"Created new answer for question {answer.QuestionId}, answerId: {addedAnswer.Id}");

            return SendResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAnswer([FromBody] Answer answer)
        {
            var result = new ServiceResponse<Answer>(System.Net.HttpStatusCode.OK);
            var updatedAnswer = await _answerRepository.UpdateAnswerAsync(answer);
            result.ResponseContent = updatedAnswer;

            _logger.LogInformation($"Updated answer {answer.Id} in question {answer.QuestionId}");

            return SendResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer([FromRoute] int id)
        {
            var result = new ServiceResponse<Answer>(System.Net.HttpStatusCode.OK);
            await _answerRepository.DeleteAnswerAsync(id);

            _logger.LogInformation($"Removed answer with answerId: {id}");

            return SendResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnswerById([FromRoute] int id)
        {
            var answer = await _answerRepository.GetAnswerByIdAsync(id);
            var result = new ServiceResponse<Answer>(System.Net.HttpStatusCode.OK)
            {
                ResponseContent = answer
            };

            _logger.LogInformation($"Answer with answerId: {id} requested");

            return SendResponse(result);
        }
    }
}
