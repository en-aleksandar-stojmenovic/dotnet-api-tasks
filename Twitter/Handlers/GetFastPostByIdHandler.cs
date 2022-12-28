using MediatR;
using Twitter.Domain;
using Twitter.Queries;
using Twitter.Repositories.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Twitter.Handlers
{
	public class GetFastPostByIdHandler : IRequestHandler<GetFastPostByIdQuery, FastPost>
	{
		private readonly IRepositoryWrapper _repository;

		public GetFastPostByIdHandler(IRepositoryWrapper repository)
		{
			_repository = repository;
		}

		public async Task<FastPost> Handle(GetFastPostByIdQuery request, CancellationToken cancellationToken)
		{
			var fastPost = await _repository.FastPost.FindFastPostByIdAsync(request.Id);

			return fastPost;
		}
	}
}
