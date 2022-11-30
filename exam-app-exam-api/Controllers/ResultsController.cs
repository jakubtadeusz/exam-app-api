using exam_app_exam_api_host.Utilities;
using exam_app_exam_api_host.ViewModels;
using ExamApp.Domain.Models;
using ExamApp.Intrastructure.Repository.Interfaces;
using ExamApp.Intrastructure.ServiceBus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exam_app_exam_api_host.Controllers
{
    public class ResultsController : BaseController
    {
        private readonly IResultRepository _resultRepository;
        private readonly ServiceBusMessager _serviceBusMessager;

        public ResultsController(IResultRepository resultRepository, ServiceBusMessager serviceBusMessager)
        {
            _resultRepository = resultRepository;
            _serviceBusMessager = serviceBusMessager;
        }

        [HttpGet]
        public async Task<IActionResult> GetResults([FromQuery] int examId)
        {
            var results = await _resultRepository.GetResultsAsync(examId);

            var resultsViewModel = new Dictionary<int, ResultsViewModel>();
            
            foreach (var result in results)
            {
                {
                    if (!resultsViewModel.ContainsKey(result.ExaminedId))
                    {
                        resultsViewModel.Add(
                            result.ExaminedId, 
                            new ResultsViewModel() {
                                UserId = result.ExaminedId,
                                Answers = new List<ResultViewModel>()
                            });
                    }
                    AddResult(resultsViewModel[result.ExaminedId], result);
                }
            }

            var response = new ServiceResponse<IEnumerable<ResultsViewModel>>(System.Net.HttpStatusCode.OK)
            {
                ResponseContent = resultsViewModel.Values.ToList()
            };

            return SendResponse(response);
        }

        private void AddResult(ResultsViewModel resultsViewModel, Result result)
        {
            if (string.IsNullOrEmpty(result.Answer)) result.Answer = string.Empty;

            foreach (var resultViewModel in resultsViewModel.Answers)
            {
                if (resultViewModel.QuestionId == result.QuestionId)
                {
                    resultViewModel.Answers.Add(result.Answer);
                    return;
                }
            }

            resultsViewModel.Answers.Add(new ResultViewModel()
            {
                QuestionId = result.QuestionId,
                Answers = new List<string>() { result.Answer },
                ResultId = result.Id
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddResult([FromBody] List<Result> results)
        {
            foreach (var result in results)
            {
                result.Id = 0;
                result.Score = 0;
            }
            
            var resp = await _resultRepository.CreateResultsAsync(results);
            var serviceResponse = new ServiceResponse<List<Result>>(System.Net.HttpStatusCode.OK)
            {
                ResponseContent = resp
            };

            return SendResponse(serviceResponse);
        }
        
        [HttpPost("grade")]
        public async Task<IActionResult> GradeResult([FromBody] List<Result> results)
        {
            var gradedResult = await _resultRepository.GradeResultsAsync(results);
            var serviceResponse = new ServiceResponse<List<Result>>(System.Net.HttpStatusCode.OK)
            {
                ResponseContent = gradedResult
            };

            return SendResponse(serviceResponse);
        }
        
        [HttpPost("grade/finish/{examId}")]
        public async Task<IActionResult> FinishGrading([FromRoute] int examId)
        {
            await _serviceBusMessager.SendFinishGradingCommand(examId);

            return SendResponse(new ServiceResponse(System.Net.HttpStatusCode.OK));
        }
    }
}
