using MediatR;
using ProjectSetup.Domain;
using System.Collections.Generic;

namespace ProjectSetup.Queries
{
	public class GetAllFastPostsQuery : IRequest<List<FastPost>>
	{
	}
}
