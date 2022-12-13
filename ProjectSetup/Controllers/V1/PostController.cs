﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectSetup.Contracts.V1;
using ProjectSetup.Contracts.V1.Requests;
using ProjectSetup.Data;
using ProjectSetup.Domain;
using ProjectSetup.Exceptions;
using ProjectSetup.Exceptions.ExceptionFilters;
using ProjectSetup.Extensions;
using ProjectSetup.Repositories.Interfaces;
using ProjectSetup.Services;
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
		private readonly ApplicationDbContext _context;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;

		public PostController(IRepositoryWrapper repository, ApplicationDbContext context, ILoggerManager logger, IMapper mapper)
		{
			_repository = repository;
			_context = context;
			_logger = logger;
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
		[CustomExceptionFilter(typeof(PostBadRequestException), HttpStatusCode.BadRequest)]
		[CustomExceptionFilter(typeof(CategoryBadRequestException), HttpStatusCode.BadRequest)]
		public async Task<IActionResult> Update([FromBody] UpdatePostRequest postRequest)
		{
			var post = await _repository.Post.UpdatePost(postRequest);

			await _repository.SaveAsync();

			return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
		}
	}
}
