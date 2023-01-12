using FluentResults;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Twitter.Domain;
using Twitter.Queries;
using Twitter.Repositories.Interfaces;

namespace Twitter.Handlers
{
	public class GetAllFastPostsHandler : IRequestHandler<GetAllFastPostsQuery, Result<List<FastPost>>>
	{
		private readonly IRepositoryWrapper _repository;

		public GetAllFastPostsHandler(IRepositoryWrapper repository)
		{
			_repository = repository;
		}

		public async Task<Result<List<FastPost>>> Handle(GetAllFastPostsQuery request, CancellationToken cancellationToken)
		{
			var fastPost = await _repository.FastPost.FindAllFastPostsAsync();

			return fastPost;
		}
	}
}
