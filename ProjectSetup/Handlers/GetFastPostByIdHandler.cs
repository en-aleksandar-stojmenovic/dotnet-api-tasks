using MediatR;
using ProjectSetup.Domain;
using ProjectSetup.Queries;
using ProjectSetup.Repositories.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectSetup.Handlers
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
