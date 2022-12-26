using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectSetup.Commands;
using ProjectSetup.Contracts.V1;
using ProjectSetup.Exceptions;
using ProjectSetup.Exceptions.ExceptionFilters;
using System.Net;
using System.Threading.Tasks;

namespace ProjectSetup.Controllers.V1
{
	[ApiController]
	public class FastPostController : ControllerBase
	{
		private readonly IMediator _mediator;

		public FastPostController(IMediator mediatr)
		{
			_mediator = mediatr;
		}

		[HttpPost(ApiRoutes.FastPost.Create)]
		[CustomExceptionFilter(typeof(CategoryBadRequestException), HttpStatusCode.BadRequest)]
		public async Task<IActionResult> Create([FromBody] CreateFastPostCommand command)
		{
			var result = await _mediator.Send(command);

			return Ok(result);
		}
	}
}
