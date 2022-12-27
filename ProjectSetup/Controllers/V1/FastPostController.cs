using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectSetup.Commands;
using ProjectSetup.Contracts.V1;
using ProjectSetup.Domain;
using ProjectSetup.Exceptions;
using ProjectSetup.Exceptions.ExceptionFilters;
using ProjectSetup.Queries;
using System;
using System.Collections.Generic;
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

		[HttpGet(ApiRoutes.FastPost.GetPost)]
		[CustomExceptionFilter(typeof(FastPostNotFoundException), HttpStatusCode.NotFound)]
		public async Task<ActionResult<FastPost>> ReadFastPost([FromRoute] Guid postId)
		{
			var query = new GetFastPostByIdQuery(postId);

			var result = await _mediator.Send(query);

			return Ok(result);
		}

		[HttpGet(ApiRoutes.FastPost.GetAll)]
		[CustomExceptionFilter(typeof(PostNotFoundException), HttpStatusCode.NotFound)]
		public async Task<ActionResult<List<FastPost>>> ReadAllFastPosts()
		{
			var query = new GetAllFastPostsQuery();

			var result = await _mediator.Send(query);

			return Ok(result);
		}
	}
}
