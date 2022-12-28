using MediatR;
using Twitter.Domain;
using Twitter.Queries;
using Twitter.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Twitter.Handlers
{
	public class GetAllFastPostsHandler : IRequestHandler<GetAllFastPostsQuery, List<FastPost>>
	{
		private readonly IRepositoryWrapper _repository;

		public GetAllFastPostsHandler(IRepositoryWrapper repository)
		{
			_repository = repository;
		}

		public async Task<List<FastPost>> Handle(GetAllFastPostsQuery request, CancellationToken cancellationToken)
		{
			var fastPost = await _repository.FastPost.FindAllFastPostsAsync();

			return fastPost;
		}
	}
}
