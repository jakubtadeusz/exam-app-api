using exam_app_exam_api_host.Utilities;
using ExamApp.Domain.Models;
using ExamApp.Intrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace exam_app_exam_api_host.Controllers
{
    public class AnswersController : BaseController
    {
        private readonly IAnswerRepository _answerRepository;

        public AnswersController(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnswers([FromQuery] int questionId)
        {
            var answers = await _answerRepository.GetAnswersAsync(questionId);
            var result = new ServiceResponse<IEnumerable<Answer>>(System.Net.HttpStatusCode.OK)
            {
                ResponseContent = answers
            };

            return SendResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnswer([FromBody] Answer answer)
        {
            var result = new ServiceResponse<Answer>(System.Net.HttpStatusCode.OK);
            var addedAnswer = await _answerRepository.CreateAnswerAsync(answer);
            result.ResponseContent = addedAnswer;

            return SendResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAnswer([FromBody] Answer answer)
        {
            var result = new ServiceResponse<Answer>(System.Net.HttpStatusCode.OK);
            var updatedAnswer = await _answerRepository.UpdateAnswerAsync(answer);
            result.ResponseContent = updatedAnswer;

            return SendResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer([FromRoute] int id)
        {
            var result = new ServiceResponse<Answer>(System.Net.HttpStatusCode.OK);
            await _answerRepository.DeleteAnswerAsync(id);

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

            return SendResponse(result);
        }
    }
}
