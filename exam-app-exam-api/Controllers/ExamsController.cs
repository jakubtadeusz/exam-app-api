using exam_app_exam_api_host.Utilities;
using ExamApp.Domain.Models;
using ExamApp.Intrastructure.Repository.Interfaces;
using ExamApp.Intrastructure.ServiceBus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace exam_app_exam_api_host.Controllers
{
    public class ExamsController : BaseController
    {
        private readonly IExamRepository _examRepository;
        private readonly ServiceBusMessager _serviceBusMessager;

        public ExamsController(IExamRepository examRepository, ServiceBusMessager serviceBusMessager)
        {
            _examRepository = examRepository;
            _serviceBusMessager = serviceBusMessager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ownerId = GetUserGuid(this.User);
            var exams = await _examRepository.GetExamsAsync(ownerId);

            var response = new ServiceResponse<IEnumerable<Exam>>(System.Net.HttpStatusCode.OK)
            {
                ResponseContent = exams
            };

            return SendResponse(response);
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
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
            var ownerId = GetUserGuid(this.User);
            exam.OwnerId = ownerId;
            var createdExam = await _examRepository.CreateExamAsync(exam);

            var response = new ServiceResponse<Exam>(System.Net.HttpStatusCode.OK)
            {
                ResponseContent = createdExam
            };

            return SendResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromQuery] int examId, [FromBody] Exam exam)
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

        [HttpPost("start/{examId}")]
        public async Task<IActionResult> StartExam([FromRoute] int examId)
        {
            await _serviceBusMessager.SendStartExamMessage(examId);

            var response = new ServiceResponse<Exam>(System.Net.HttpStatusCode.OK);

            return SendResponse(response);
        }

        private Guid GetUserGuid(ClaimsPrincipal user)
        {
            var claims = user.Claims;
            var ownerId = new Guid(claims.FirstOrDefault(x => x.Type == "id").Value);
            return ownerId;
        }
    }
}
