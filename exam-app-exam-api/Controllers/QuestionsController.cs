using exam_app_exam_api_host.Utilities;
using ExamApp.Domain.Models;
using ExamApp.Intrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace exam_app_exam_api_host.Controllers
{
    public class QuestionsController : BaseController
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionsController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestions([FromQuery] int examId)
        {
            var questions = await _questionRepository.GetQuestionsAsync(examId);
            var result = new ServiceResponse<IEnumerable<Question>>(System.Net.HttpStatusCode.OK)
            {
                ResponseContent = questions
            };

            return SendResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion([FromBody] Question question)
        {
            var result = new ServiceResponse<Question>(System.Net.HttpStatusCode.OK);
            var addedQuestion = await _questionRepository.AddQuestionAsync(question);
            result.ResponseContent = addedQuestion;

            return SendResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuestion([FromBody] Question question)
        {
            var result = new ServiceResponse<Question>(System.Net.HttpStatusCode.OK);
            var updatedQuestion = await _questionRepository.UpdateQuestionAsync(question);
            result.ResponseContent = updatedQuestion;

            return SendResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion([FromRoute] int id)
        {
            var result = new ServiceResponse<Question>(System.Net.HttpStatusCode.OK);
            await _questionRepository.DeleteQuestionAsync(id);

            return SendResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById([FromRoute] int id)
        {
            var result = new ServiceResponse<Question>(System.Net.HttpStatusCode.OK);
            var question = await _questionRepository.GetQuestionByIdAsync(id);
            result.ResponseContent = question;

            return SendResponse(result);
        }
    }
}
