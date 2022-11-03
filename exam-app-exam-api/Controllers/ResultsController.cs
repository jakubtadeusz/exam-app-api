using exam_app_exam_api_host.Utilities;
using ExamApp.Domain.Models;
using ExamApp.Intrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace exam_app_exam_api_host.Controllers
{
    public class ResultsController : BaseController
    {
        private readonly IResultRepository _resultRepository;

        public ResultsController(IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetResults([FromQuery] int examId)
        {
            var results = await _resultRepository.GetResultsAsync(examId);
            var result = new ServiceResponse<IEnumerable<Result>>(System.Net.HttpStatusCode.OK)
            {
                ResponseContent = results
            };

            return SendResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddResults([FromBody] List<Result> result)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> GradeResult([FromBody] Result result)
        {
            {
                var gradedResult = result; //await _resultRepository.GradeResultAsync(result);
                var serviceResponse = new ServiceResponse<Result>(System.Net.HttpStatusCode.OK)
                {
                    ResponseContent = gradedResult
                };

                return SendResponse(serviceResponse);
            }
        }
    }
}
