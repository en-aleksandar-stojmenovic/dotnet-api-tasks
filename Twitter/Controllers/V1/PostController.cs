using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Twitter.Contracts.V1;
using Twitter.Contracts.V1.Requests;
using Twitter.Contracts.V1.Responses;
using Twitter.Domain;
using Twitter.Exceptions;
using Twitter.Exceptions.ExceptionFilters;
using Twitter.Extensions;
using Twitter.Queries;
using Twitter.Repositories.Interfaces;

namespace Twitter.Controllers.V1
{
	[ApiController]
	[Produces("application/json")]
	public class PostController : ControllerBase
	{
		private readonly IRepositoryWrapper _repository;
		private readonly IMapper _mapper;

		public PostController(IRepositoryWrapper repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		/// <summary>
		/// Returns all posts.
		/// </summary>
		/// <returns>Returns all posts.</returns>
		/// <response code = "200">Returns a list of posts.</response>
		[HttpGet(ApiRoutes.Post.GetAll)]
		[ProducesResponseType(typeof(List<Post>), 200)]
		public async Task<ActionResult<List<Post>>> Get()
		{
			return Ok(await _repository.Post.FindAllPostsAsync());
		}

		/// <summary>
		/// Returns posts with pagination.
		/// </summary>
		/// <returns>Returns posts with pagination.</returns>
		/// <response code = "200">Returns a list of posts with pagination.</response>
		[HttpGet(ApiRoutes.Post.ReadAll)]
		[ProducesResponseType(typeof(PagedResponse<Post>), 200)]
		public async Task<IActionResult> ReadAllPosts([FromQuery] PaginationQuery paginationQuary)
		{
			var paginationFilter = _mapper.Map<PaginationFilter>(paginationQuary);

			var posts = await _repository.Post.ReadAllPostsAsync(paginationFilter);

			var paginationResponse = new PagedResponse<Post>
			{
				Data = posts,
				PageNumber = paginationFilter.PageNumber,
				PageSize = paginationFilter.PageSize,
			};

			return Ok(paginationResponse);
		}

		/// <summary>
		/// Returns a post.
		/// </summary>
		/// <returns>Returns a post.</returns>
		/// <response code = "200">Returns a post if it is found.</response>
		/// <response code = "404">Throws an exception if post doesn't exist.</response>
		[HttpGet(ApiRoutes.Post.GetPost)]
		[ProducesResponseType(typeof(Post), 200)]
		[ProducesResponseType(typeof(ErrorDetails), 404)]
		[CustomExceptionFilter(typeof(PostNotFoundException), HttpStatusCode.NotFound)]
		public async Task<ActionResult<Post>> Get([FromRoute] Guid postId)
		{
			return Ok(await _repository.Post.FindPostByIdAsync(postId));
		}

		/// <summary>
		/// Creates a post. Only users with admin role can create new post.
		/// </summary>
		/// <returns>Returns created post.</returns>
		/// <response code = "200">Returns created post.</response>
		/// <response code = "400">Throws an exception if category id doesn't exist.</response>
		[Authorize(Roles = UserRoles.Admin)]
		[HttpPost(ApiRoutes.Post.Create)]
		[ProducesResponseType(typeof(Post), 200)]
		[ProducesResponseType(typeof(ErrorDetails), 400)]
		[CustomExceptionFilter(typeof(CategoryBadRequestException), HttpStatusCode.BadRequest)]
		public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
		{
			var mappedPost = _mapper.Map<Post>(postRequest);

			mappedPost.CreatedBy = HttpContext.GetUserId();

			var post = await _repository.Post.CreatePost(mappedPost);

			await _repository.SaveAsync();

			return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
		}

		/// <summary>
		/// Deletes a post. Only users with admin role can delete a post.
		/// </summary>
		/// <returns>Returns true if a post is deleted.</returns>
		/// <response code = "200">Returns true if a post is deleted.</response>
		/// <response code = "400">Throws an exception if a post doesn't exist.</response>
		[Authorize(Roles = UserRoles.Admin)]
		[HttpDelete(ApiRoutes.Post.Delete)]
		[ProducesResponseType(typeof(bool), 200)]
		[ProducesResponseType(typeof(ErrorDetails), 400)]
		[CustomExceptionFilter(typeof(PostBadRequestException), HttpStatusCode.BadRequest)]
		public async Task<IActionResult> DeletePostById([FromRoute] Guid postId)
		{
			_repository.Post.DeletePost(postId);

			var deleted = await _repository.SaveAsync();

			return Ok(deleted > 0);
		}

		/// <summary>
		/// Updates a post. Users with admin role can update only their own posts.
		/// </summary>
		/// <returns>Returns updated post.</returns>
		/// <response code = "200">Returns updated post.</response>
		/// <response code = "400">Throws an exception if user doesn't own the post, or if category id doesn't exist.</response>
		/// <response code = "404">Throws an exception if post doesn't exist.</response>
		[Authorize(Roles = UserRoles.Admin)]
		[HttpPut(ApiRoutes.Post.Update)]
		[ProducesResponseType(typeof(bool), 200)]
		[ProducesResponseType(typeof(ErrorDetails), 400)]
		[ProducesResponseType(typeof(ErrorDetails), 404)]
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

		/// <summary>
		/// Returns a number of available posts.
		/// </summary>
		/// <returns>Returns a number of available posts.</returns>
		/// <response code = "200">Returns a number of available posts.</response>
		[HttpGet(ApiRoutes.Post.NumberOfAvailablePosts)]
		[ProducesResponseType(typeof(int), 200)]
		public async Task<ActionResult<int>> NumberOfAvailablePosts([FromQuery] Guid? categoryId)
		{
			return Ok(await _repository.Post.NumberOfAvailablePosts(categoryId));
		}
	}
}
