using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using Twitter.Domain;

namespace Twitter.Contracts.V1.Examples.Responses
{
	public class ListFastPostResponseExample : IExamplesProvider<List<FastPost>>
	{
		public List<FastPost> GetExamples()
		{
			var fastPosts = new List<FastPost>();

			fastPosts.Add(new FastPost
			{
				Id = Guid.NewGuid(),
				Text = "Some text",
				Created = DateTime.Now,
				CategoryId = Guid.NewGuid(),
				CreatedBy = Guid.NewGuid(),
			});

			fastPosts.Add(new FastPost
			{
				Id = Guid.NewGuid(),
				Text = "Some text",
				Created = DateTime.Now,
				CategoryId = Guid.NewGuid(),
				CreatedBy = Guid.NewGuid(),
			});

			return fastPosts;
		}
	}
}
