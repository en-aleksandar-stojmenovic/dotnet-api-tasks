using Microsoft.AspNetCore.Mvc;
using ProjectSetup.Contracts.V1;
using ProjectSetup.Contracts.V1.Requests;
using ProjectSetup.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectSetup.Controllers.V1
{
	[ApiController]
	public class PostController : ControllerBase
	{
		private readonly IPostRepository _repository;

		public PostController(IPostRepository repository)
		{
			_repository = repository;
		}

		[HttpGet(ApiRoutes.Post.GetAll)]
		public async Task<ActionResult<List<Post>>> Get()
		{
			throw new NotImplementedException();
		}

		[HttpGet(ApiRoutes.Post.GetPost)]
		public async Task<ActionResult<Post>> Get([FromRoute] Guid postId)
		{
			throw new NotImplementedException();
		}

		[HttpPost(ApiRoutes.Post.Create)]
		public async Task<IActionResult> Create([FromBody] CreatePostRequest post)
		{
			throw new NotImplementedException();
		}

		[HttpDelete(ApiRoutes.Post.Delete)]
		public async Task<IActionResult> DeleteCategoryById([FromRoute] Guid postId)
		{
			throw new NotImplementedException();
		}

		[HttpPut(ApiRoutes.Post.Update)]
		public async Task<IActionResult> Update([FromBody] UpdatePostRequest post)
		{
			throw new NotImplementedException();
		}
	}
}
