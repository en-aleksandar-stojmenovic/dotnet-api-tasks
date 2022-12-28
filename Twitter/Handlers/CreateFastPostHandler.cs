using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Twitter.Commands;
using Twitter.Domain;
using Twitter.Extensions;
using Twitter.Repositories.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Twitter.Handlers
{
	public class CreateFastPostHandler : IRequestHandler<CreateFastPostCommand, FastPost>
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

		public async Task<FastPost> Handle(CreateFastPostCommand request, CancellationToken cancellationToken)
		{
			var mappedFastPost = _mapper.Map<FastPost>(request);

			mappedFastPost.CreatedBy = _httpContextAccessor.HttpContext.GetUserId();

			var post = await _repository.FastPost.CreateFastPost(mappedFastPost);

			await _repository.SaveAsync();

			return post;
		}
	}
}
