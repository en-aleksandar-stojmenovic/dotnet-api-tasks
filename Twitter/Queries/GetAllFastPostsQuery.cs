using MediatR;
using Twitter.Domain;
using System.Collections.Generic;

namespace Twitter.Queries
{
	public class GetAllFastPostsQuery : IRequest<List<FastPost>>
	{
	}
}
