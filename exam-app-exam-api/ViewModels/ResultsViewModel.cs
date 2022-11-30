namespace exam_app_exam_api_host.ViewModels
{
    public class ResultsViewModel
    {
        public int UserId { get; set; }
        public IList<ResultViewModel> Answers { get; set; }
    }

    public class ResultViewModel
    {
        public int QuestionId { get; set; }
        public List<string> Answers { get; set; } = new List<string>();
        public int Result { get; set; }
        public int ResultId { get; set; }
    }
}
