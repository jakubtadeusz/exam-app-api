using exam_app_exam_api_host.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace exam_app_exam_api_host.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected IActionResult SendResponse(ServiceResponse response)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.OK => NoContent(),
                HttpStatusCode.Unauthorized => Unauthorized(),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.NotFound => NotFound(),
                _ => BadRequest(new { Errors = response.Errors })
            };
        }

        protected IActionResult SendResponse<T>(ServiceResponse<T> response)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(response.ResponseContent),
                HttpStatusCode.Unauthorized => Unauthorized(),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.NotFound => NotFound(),
                _ => BadRequest(new { Errors = new { response.Errors } })
            };
        }
        protected Guid GetUserGuid(ClaimsPrincipal user)
        {
            var claims = user.Claims;
            var ownerId = new Guid(claims.FirstOrDefault(x => x.Type == "id").Value);
            return ownerId;
        }
    }
}