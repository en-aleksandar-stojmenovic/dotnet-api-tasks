using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;
using Twitter.Commands;
using Twitter.Domain;
using Twitter.Extensions;
using Twitter.Repositories.Interfaces;

namespace Twitter.Handlers
{
	public class CreateFastPostHandler : IRequestHandler<CreateFastPostCommand, Result<FastPost>>
	{
		private readonly IRepositoryWrapper _repository;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CreateFastPostHandler(IRepositoryWrapper repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
		{
			_repository = repository;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<Result<FastPost>> Handle(CreateFastPostCommand request, CancellationToken cancellationToken)
		{
			var mappedFastPost = _mapper.Map<FastPost>(request);

			mappedFastPost.CreatedBy = _httpContextAccessor.HttpContext.GetUserId();

			var result = await _repository.FastPost.CreateFastPost(mappedFastPost);

			if (result.IsSuccess)
			{
				await _repository.SaveAsync();
			}

			return result;
		}
	}
}
