using MediatR;
using ProjectSetup.Domain;
using System;

namespace ProjectSetup.Queries
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
