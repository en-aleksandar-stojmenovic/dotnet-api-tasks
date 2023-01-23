using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Twitter.Commands;
using Twitter.Contracts.V1.Requests;
using Twitter.Contracts.V1.Responses;
using Twitter.Repositories.Interfaces;

namespace Twitter.Handlers
{
	public class LoginHandler : IRequestHandler<LoginCommand, AuthResponse>
	{
		private readonly IRepositoryWrapper _repository;
		private readonly IMapper _mapper;

		public LoginHandler(IRepositoryWrapper repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			var mappedLoginRequest = _mapper.Map<LoginUserRequest>(request);

			var response = await _repository.User.Login(mappedLoginRequest);

			var user = await _repository.User.FindUserByUsernameAsync(mappedLoginRequest.Username);

			response.Posts = await _repository.Post.FindUserPostsAsync(Guid.Parse(user.Id));

			return response;
		}
	}
}
