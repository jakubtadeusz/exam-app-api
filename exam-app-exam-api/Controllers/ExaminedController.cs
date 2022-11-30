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
        public async Task<IActionResult> GetExamined()
        {
            var ownerId = GetUserGuid(this.User);
            var examined = await _examinedRepository.GetExaminedAsync(ownerId);
            var result = new ServiceResponse<IEnumerable<Examined>>(System.Net.HttpStatusCode.OK)
            {
                ResponseContent = examined
            };

            return SendResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddExamined([FromBody] Examined examined)
        {
            var ownerId = GetUserGuid(this.User);
            examined.OwnerId = ownerId;
            var result = new ServiceResponse<Examined>(System.Net.HttpStatusCode.OK);
            var addedExamined = await _examinedRepository.AddExaminedAsync(examined);
            result.ResponseContent = addedExamined;
            
            return SendResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExamined([FromRoute] int id, [FromBody] Examined examined)
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
            var ownerId = GetUserGuid(this.User);
            var examined = await _examinedRepository.GetExaminedAsync(ownerId, groupName);
            var result = new ServiceResponse<IEnumerable<Examined>>(System.Net.HttpStatusCode.OK)
            {
                ResponseContent = examined
            };

            return SendResponse(result);
        }

        [HttpGet("groups")]
        public async Task<IActionResult> GetGroups()
        {
            {
                var ownerId = GetUserGuid(this.User);
                var groups = await _examinedRepository.GetGroupsAsync(ownerId);
                var result = new ServiceResponse<IEnumerable<string>>(System.Net.HttpStatusCode.OK)
                {
                    ResponseContent = groups
                };

                return SendResponse(result);
            }
        }
    }
}
