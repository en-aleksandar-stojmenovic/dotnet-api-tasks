using Microsoft.AspNetCore.Mvc;
using Twitter.Contracts.V1;
using Twitter.Contracts.V1.Requests;
using Twitter.Exceptions;
using Twitter.Exceptions.ExceptionFilters;
using Twitter.Repositories.Interfaces;
using System.Net;
using System.Threading.Tasks;

namespace Twitter.Controllers.V1
{
	[ApiController]
	public class IdentityController : ControllerBase
	{

		private readonly IRepositoryWrapper _repository;

		public IdentityController(IRepositoryWrapper repository)
		{
			_repository = repository;
		}

		[HttpPost(ApiRoutes.User.Register)]
		[CustomExceptionFilter(typeof(UserBadRequestException), HttpStatusCode.BadRequest)]
		public async Task<IActionResult> Register([FromBody] RegisterUserRequest userRequest)
		{
			var user = await _repository.User.CreateUser(userRequest);

			await _repository.SaveAsync();

			return Ok(user);
		}

		[HttpPost(ApiRoutes.User.Login)]
		[CustomExceptionFilter(typeof(UserBadRequestException), HttpStatusCode.Unauthorized)]
		public async Task<IActionResult> Login([FromBody] LoginUserRequest userRequest)
		{
			return Ok(await _repository.User.Login(userRequest));
		}
	}
}
