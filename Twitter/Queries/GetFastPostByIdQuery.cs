using FluentResults;
using MediatR;
using System;
using Twitter.Domain;

namespace Twitter.Queries
{
	public class GetFastPostByIdQuery : IRequest<Result<FastPost>>
	{
		public Guid Id { get; set; }

		public GetFastPostByIdQuery(Guid id)
		{
			Id = id;
		}
	}
}
