using exam_app_exam_api_host.Utilities;
using ExamApp.Domain.Models;
using ExamApp.Intrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace exam_app_exam_api_host.Controllers
{
    public class ExamsController : BaseController
    {
        private readonly IExamRepository _examRepository;

        public ExamsController(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery][Required] int ownerId)
        {
            var exams = await _examRepository.GetExamsAsync(ownerId);

            var response = new ServiceResponse<IEnumerable<Exam>>(System.Net.HttpStatusCode.OK)
            {
                ResponseContent = exams
            };

            return SendResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var exam = await _examRepository.GetExamByIdAsync(id);

            var response = new ServiceResponse<Exam>(System.Net.HttpStatusCode.OK)
            {
                ResponseContent = exam
            };

            return SendResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Exam exam)
        {
            var createdExam = await _examRepository.CreateExamAsync(exam);

            var response = new ServiceResponse<Exam>(System.Net.HttpStatusCode.Created)
            {
                ResponseContent = createdExam
            };

            return SendResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Exam exam)
        {
            var updatedExam = await _examRepository.UpdateExamAsync(exam);

            var response = new ServiceResponse<Exam>(System.Net.HttpStatusCode.OK)
            {
                ResponseContent = updatedExam
            };

            return SendResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _examRepository.DeleteExamAsync(id);

            var response = new ServiceResponse(System.Net.HttpStatusCode.OK);

            return SendResponse(response);
        }
    }
}
