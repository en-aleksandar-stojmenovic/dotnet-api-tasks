﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Twitter.Commands;
using Twitter.Contracts.V1;
using Twitter.Domain;
using Twitter.Exceptions;
using Twitter.Exceptions.ExceptionFilters;
using Twitter.Queries;

namespace Twitter.Controllers.V1
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
		public async Task<ActionResult<List<FastPost>>> ReadAllFastPosts()
		{
			var query = new GetAllFastPostsQuery();

			var result = await _mediator.Send(query);

			return Ok(result);
		}

		[HttpDelete(ApiRoutes.FastPost.Delete)]
		[CustomExceptionFilter(typeof(FastPostNotFoundException), HttpStatusCode.NotFound)]
		public async Task<ActionResult> DeleteFastPost([FromRoute] Guid postId)
		{
			var query = new DeleteFastPostByIdQuery(postId);

			var result = await _mediator.Send(query);

			return Ok(result);
		}
	}
}