using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Twitter.Commands;
using Twitter.Contracts.V1;
using Twitter.Contracts.V1.Requests;
using Twitter.Exceptions;
using Twitter.Exceptions.ExceptionFilters;
using Twitter.Repositories.Interfaces;

namespace Twitter.Controllers.V1
{
	[ApiController]
	public class IdentityController : ControllerBase
	{

		private readonly IRepositoryWrapper _repository;
		private readonly IMediator _mediator;

		public IdentityController(IRepositoryWrapper repository, IMediator mediator)
		{
			_repository = repository;
			_mediator = mediator;
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
		public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
		{
			return Ok(await _mediator.Send(loginCommand));
		}
	}
}
