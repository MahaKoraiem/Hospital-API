using HospitalApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected IActionResult FromServiceResult<T>(services.ServiceResult<T> result)
        {
            if (result.Success)
            {
                return Ok(Helpers.ApiResponse.Ok(result.Data, result.ErrorMessage));
            }
            else
            {
                return BadRequest(Helpers.ApiResponse.Fail(result.ErrorMessage));
            }
        }

        protected IActionResult NotFoundResponse(string message)
        {
            return NotFound(ApiResponse.Fail(message));
        }
    }
}
