using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Twitter.Queries;
using Twitter.Repositories.Interfaces;

namespace Twitter.Handlers
{
	public class DeleteFastPostByIdHandler : IRequestHandler<DeleteFastPostByIdQuery, bool>
	{
		private readonly IRepositoryWrapper _repository;

		public DeleteFastPostByIdHandler(IRepositoryWrapper repository)
		{
			_repository = repository;
		}

		public async Task<bool> Handle(DeleteFastPostByIdQuery request, CancellationToken cancellationToken)
		{
			_repository.FastPost.DeleteFastPost(request.Id);

			var deleted = await _repository.SaveAsync();

			return deleted > 0;
		}
	}
}
