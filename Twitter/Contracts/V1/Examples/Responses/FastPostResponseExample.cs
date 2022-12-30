using Swashbuckle.AspNetCore.Filters;
using System;
using Twitter.Domain;

namespace Twitter.Contracts.V1.Examples.Responses
{
	public class FastPostResponseExample : IExamplesProvider<FastPost>
	{
		public FastPost GetExamples()
		{
			return new FastPost
			{
				Id = Guid.NewGuid(),
				Text = "Some text",
				Created = DateTime.Now,
				CategoryId = Guid.NewGuid(),
				CreatedBy = Guid.NewGuid(),
			};
		}
	}
}
