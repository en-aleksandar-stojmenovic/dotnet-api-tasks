using Microsoft.AspNetCore.Mvc;
using ProjectSetup.Contracts.V1;
using ProjectSetup.Contracts.V1.Requests;
using ProjectSetup.Exceptions;
using ProjectSetup.Exceptions.ExceptionFilters;
using ProjectSetup.Repositories.Interfaces;
using System.Net;
using System.Threading.Tasks;

namespace ProjectSetup.Controllers.V1
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
