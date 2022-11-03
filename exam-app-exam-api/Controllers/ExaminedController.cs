using exam_app_exam_api_host.Utilities;
using ExamApp.Domain.Models;
using ExamApp.Intrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace exam_app_exam_api_host.Controllers
{
    public class ExaminedController : BaseController
    {
        private readonly IExaminedRepository _examinedRepository;

        public ExaminedController(IExaminedRepository examinedRepository)
        {
            _examinedRepository = examinedRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetExamined([FromQuery] int examId)
        {
            var examined = await _examinedRepository.GetExaminedAsync(examId);
            var result = new ServiceResponse<IEnumerable<Examined>>(System.Net.HttpStatusCode.OK)
            {
                ResponseContent = examined
            };

            return SendResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddExamined([FromBody] Examined examined)
        {
            var result = new ServiceResponse<Examined>(System.Net.HttpStatusCode.OK);
            var addedExamined = await _examinedRepository.AddExaminedAsync(examined);
            result.ResponseContent = addedExamined;

            return SendResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExamined([FromBody] Examined examined)
        {
            var result = new ServiceResponse<Examined>(System.Net.HttpStatusCode.OK);
            var updatedExamined = await _examinedRepository.UpdateExaminedAsync(examined);
            result.ResponseContent = updatedExamined;

            return SendResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamined([FromRoute] int id)
        {
            var result = new ServiceResponse<Examined>(System.Net.HttpStatusCode.OK);
            await _examinedRepository.DeleteExaminedAsync(id);

            return SendResponse(result);
        }

        [HttpGet("group/{groupName}")]
        public async Task<IActionResult> GetExaminedByGroupName([FromRoute] string groupName)
        {
            var examined = await _examinedRepository.GetExaminedAsync(groupName);
            var result = new ServiceResponse<IEnumerable<Examined>>(System.Net.HttpStatusCode.OK)
            {
                ResponseContent = examined
            };

            return SendResponse(result);
        }

    }
}
