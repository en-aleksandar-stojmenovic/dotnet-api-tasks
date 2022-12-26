using MediatR;
using ProjectSetup.Domain;
using ProjectSetup.Exceptions;
using ProjectSetup.Queries;
using ProjectSetup.Repositories.Interfaces;
using System;
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

			if ((DateTime.Now - fastPost.Created).TotalHours >= 24)
			{
				throw new FastPostNotFoundException("FastPost with Id: '" + fastPost.Id + "' is no longer available");
			}

			return fastPost;
		}
	}
}
