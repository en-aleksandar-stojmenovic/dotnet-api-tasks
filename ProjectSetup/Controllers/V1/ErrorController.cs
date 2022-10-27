using Microsoft.AspNetCore.Mvc;
using ProjectSetup.Contracts.V1.Responses;

namespace ProjectSetup.Controllers.V1
{
	[ApiController]
	[Route("errors/{code}")]
	public class ErrorController : ControllerBase
	{
		protected IActionResult Error(int code)
		{
			return new ObjectResult(new ApiResponse(code));
		}
	}
}
