using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ProjectSetup.Exceptions.ExceptionFilters;
using ProjectSetup.Exceptions;
using System.Net;
using ProjectSetup.Contracts.V1;
using ProjectSetup.Contracts.V1.Requests;

namespace ProjectSetup.Controllers.V1
{
	[ApiController]
	public class IdentityController: ControllerBase
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
	}
}
