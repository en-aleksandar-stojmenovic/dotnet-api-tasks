using MediatR;
using Twitter.Domain;
using System;

namespace Twitter.Queries
{
	public class GetFastPostByIdQuery : IRequest<FastPost>
	{
		public Guid Id { get; set; }

		public GetFastPostByIdQuery(Guid id)
		{
			Id = id;
		}
	}
}
