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
	[Produces("application/json")]
	public class FastPostController : ControllerBase
	{
		private readonly IMediator _mediator;

		public FastPostController(IMediator mediatr)
		{
			_mediator = mediatr;
		}

		/// <summary>
		/// Creates a fast post
		/// </summary>
		/// <returns>Returns created fast post.</returns>
		/// <response code = "200">Returns created fast post.</response>
		/// <response code = "400">Throws exception if category id doesn't exists.</response>
		[HttpPost(ApiRoutes.FastPost.Create)]
		[ProducesResponseType(typeof(List<FastPost>), 200)]
		[ProducesResponseType(typeof(ErrorDetails), 400)]
		[CustomExceptionFilter(typeof(CategoryBadRequestException), HttpStatusCode.BadRequest)]
		public async Task<IActionResult> Create([FromBody] CreateFastPostCommand command)
		{
			var result = await _mediator.Send(command);

			return Ok(result);
		}

		/// <summary>
		/// Returns a fast post. If 24 hours passed since creation, fast post should not be found.
		/// </summary>
		/// <returns>Returns a fast post.</returns>
		/// <response code = "200">Returns a fast post.</response>
		/// <response code = "404">Throws exception if fast post doesn't exists.</response>
		[HttpGet(ApiRoutes.FastPost.GetPost)]
		[ProducesResponseType(typeof(FastPost), 200)]
		[ProducesResponseType(typeof(ErrorDetails), 404)]
		[CustomExceptionFilter(typeof(FastPostNotFoundException), HttpStatusCode.NotFound)]
		public async Task<ActionResult<FastPost>> ReadFastPost([FromRoute] Guid postId)
		{
			var query = new GetFastPostByIdQuery(postId);

			var result = await _mediator.Send(query);

			return Ok(result);
		}

		/// <summary>
		/// Returns all fast posts. If 24 hours passed since creation, fast post should not be in the list.
		/// </summary>
		/// <returns>Returns all fast posts.</returns>
		/// <response code = "200">Returns a list of fast posts.</response>
		[HttpGet(ApiRoutes.FastPost.GetAll)]
		[ProducesResponseType(typeof(List<FastPost>), 200)]
		public async Task<ActionResult<List<FastPost>>> ReadAllFastPosts()
		{
			var query = new GetAllFastPostsQuery();

			var result = await _mediator.Send(query);

			return Ok(result);
		}

		/// <summary>
		/// Deletes a fast post. If 24 hours passed since creation, fast post should not be deleted.
		/// </summary>
		/// <returns>Returns true if a fast post is deleted.</returns>
		/// <response code = "200">Returns true if the fast post is deleted.</response>
		/// <response code = "404">Throws exception if a fast post doesn't exists.</response>
		[HttpDelete(ApiRoutes.FastPost.Delete)]
		[ProducesResponseType(typeof(bool), 200)]
		[ProducesResponseType(typeof(ErrorDetails), 404)]
		[CustomExceptionFilter(typeof(FastPostNotFoundException), HttpStatusCode.NotFound)]
		public async Task<ActionResult> DeleteFastPost([FromRoute] Guid postId)
		{
			var query = new DeleteFastPostByIdQuery(postId);

			var result = await _mediator.Send(query);

			return Ok(result);
		}
	}
}
