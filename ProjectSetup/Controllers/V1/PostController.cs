using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectSetup.Contracts.V1;
using ProjectSetup.Contracts.V1.Requests;
using ProjectSetup.Domain;
using ProjectSetup.Exceptions;
using ProjectSetup.Exceptions.ExceptionFilters;
using ProjectSetup.Extensions;
using ProjectSetup.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ProjectSetup.Controllers.V1
{
	[ApiController]
	public class PostController : ControllerBase
	{
		private readonly IRepositoryWrapper _repository;
		private readonly IMapper _mapper;

		public PostController(IRepositoryWrapper repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		[HttpGet(ApiRoutes.Post.GetAll)]
		public async Task<ActionResult<List<Post>>> Get()
		{
			return Ok(await _repository.Post.FindAllPostsAsync());
		}

		[HttpGet(ApiRoutes.Post.GetPost)]
		[CustomExceptionFilter(typeof(PostNotFoundException), HttpStatusCode.NotFound)]
		public async Task<ActionResult<Post>> Get([FromRoute] Guid postId)
		{
			return Ok(await _repository.Post.FindPostByIdAsync(postId));
		}

		[Authorize(Roles = UserRoles.Admin)]
		[HttpPost(ApiRoutes.Post.Create)]
		[CustomExceptionFilter(typeof(CategoryBadRequestException), HttpStatusCode.BadRequest)]
		[CustomExceptionFilter(typeof(PostBadRequestException), HttpStatusCode.BadRequest)]
		public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
		{
			var mappedPost = _mapper.Map<Post>(postRequest);

			mappedPost.CreatedBy = HttpContext.GetUserId();

			var post = await _repository.Post.CreatePost(mappedPost);

			await _repository.SaveAsync();

			return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
		}

		[Authorize(Roles = UserRoles.Admin)]
		[HttpDelete(ApiRoutes.Post.Delete)]
		[CustomExceptionFilter(typeof(PostBadRequestException), HttpStatusCode.BadRequest)]
		public async Task<IActionResult> DeletePostById([FromRoute] Guid postId)
		{
			_repository.Post.DeletePost(postId);

			var deleted = await _repository.SaveAsync();

			return Ok(deleted > 0);
		}

		[Authorize(Roles = UserRoles.Admin)]
		[HttpPut(ApiRoutes.Post.Update)]
		[CustomExceptionFilter(typeof(PostNotFoundException), HttpStatusCode.NotFound)]
		[CustomExceptionFilter(typeof(CategoryBadRequestException), HttpStatusCode.BadRequest)]
		public async Task<IActionResult> Update([FromBody] UpdatePostRequest updateRequest)
		{
			var userOwnsPost = await _repository.Post.UserOwnsPostAsync(updateRequest.Id, HttpContext.GetUserId());

			if (!userOwnsPost)
			{
				return BadRequest("You don't own this post");
			}

			var post = await _repository.Post.FindPostByIdAsync(updateRequest.Id);

			var mappedPost = _mapper.Map(updateRequest, post);

			var updatedPost = await _repository.Post.UpdatePost(mappedPost);

			await _repository.SaveAsync();

			return CreatedAtAction(nameof(Get), new { id = updatedPost.Id }, updatedPost);
		}
	}
}
