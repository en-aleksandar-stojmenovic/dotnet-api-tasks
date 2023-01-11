using FluentResults;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Twitter.Domain;
using Twitter.Queries;
using Twitter.Repositories.Interfaces;

namespace Twitter.Handlers
{
	public class GetFastPostByIdHandler : IRequestHandler<GetFastPostByIdQuery, Result<FastPost>>
	{
		private readonly IRepositoryWrapper _repository;

		public GetFastPostByIdHandler(IRepositoryWrapper repository)
		{
			_repository = repository;
		}

		public async Task<Result<FastPost>> Handle(GetFastPostByIdQuery request, CancellationToken cancellationToken)
		{
			var fastPost = await _repository.FastPost.FindFastPostByIdAsync(request.Id);

			return fastPost;
		}
	}
}
