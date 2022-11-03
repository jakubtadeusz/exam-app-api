using System.Net;

namespace exam_app_exam_api_host.Utilities
{
    public class ServiceResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public List<string>? Errors { get; set; }

        public ServiceResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public ServiceResponse(HttpStatusCode statusCode, List<string> errors)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
    }

    public class ServiceResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public List<string>? Errors { get; set; }
        public T? ResponseContent { get; set; }

        public ServiceResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public ServiceResponse(HttpStatusCode statusCode, List<string> errors)
        {
            StatusCode = statusCode;
            Errors = errors;
        }

        public ServiceResponse(HttpStatusCode statusCode, T responseContent)
        {
            StatusCode = statusCode;
            ResponseContent = responseContent;
        }
    }
}