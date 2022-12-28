using MediatR;
using System;

namespace Twitter.Queries
{
	public class DeleteFastPostByIdQuery : IRequest<bool>
	{
		public Guid Id { get; set; }

		public DeleteFastPostByIdQuery(Guid id)
		{
			Id = id;
		}
	}
}
