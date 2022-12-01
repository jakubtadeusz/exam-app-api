using exam_app_exam_api_host.Utilities;
using ExamApp.Domain.Models;
using ExamApp.Intrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exam_app_exam_api_host.Controllers
{
    public class QuestionsController : BaseController
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ILogger<QuestionsController> _logger;

        public QuestionsController(IQuestionRepository questionRepository, ILogger<QuestionsController> logger)
        {
            _questionRepository = questionRepository;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetQuestions([FromQuery] int examId)
        {
            var questions = await _questionRepository.GetQuestionsAsync(examId);
            var result = new ServiceResponse<IEnumerable<Question>>(System.Net.HttpStatusCode.OK)
            {
                ResponseContent = questions
            };

            _logger.LogTrace($"Questions for exam {examId} requested");

            return SendResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion([FromBody] Question question)
        {
            var result = new ServiceResponse<Question>(System.Net.HttpStatusCode.OK);
            var addedQuestion = await _questionRepository.AddQuestionAsync(question);
            result.ResponseContent = addedQuestion;

            _logger.LogInformation($"Question {addedQuestion.Id} created");

            return SendResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion([FromQuery] int id, [FromBody] Question question)
        {
            var result = new ServiceResponse<Question>(System.Net.HttpStatusCode.OK);
            var updatedQuestion = await _questionRepository.UpdateQuestionAsync(question);
            result.ResponseContent = updatedQuestion;

            _logger.LogInformation($"Edited question {id}");

            return SendResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuestions([FromBody] List<Question> questions)
        {
            var result = new ServiceResponse<List<Question>>(System.Net.HttpStatusCode.OK);
            var updatedQuestions = await _questionRepository.UpdateQuestionsAsync(questions);
            result.ResponseContent = updatedQuestions;

            _logger.LogInformation($"Updated {updatedQuestions.Count} questions");

            return SendResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion([FromRoute] int id)
        {
            var result = new ServiceResponse<Question>(System.Net.HttpStatusCode.OK);
            await _questionRepository.DeleteQuestionAsync(id);

            _logger.LogInformation($"Removed question {id}");

            return SendResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById([FromRoute] int id)
        {
            var result = new ServiceResponse<Question>(System.Net.HttpStatusCode.OK);
            var question = await _questionRepository.GetQuestionByIdAsync(id);
            result.ResponseContent = question;

            _logger.LogInformation($"Requested queston {id}");

            return SendResponse(result);
        }
    }
}
