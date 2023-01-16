using FluentResults;
using MediatR;
using System.Collections.Generic;
using Twitter.Domain;

namespace Twitter.Queries
{
	public class GetAllFastPostsQuery : IRequest<Result<List<FastPost>>>
	{
	}
}
