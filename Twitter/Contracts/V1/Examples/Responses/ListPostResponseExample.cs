using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using Twitter.Domain;

namespace Twitter.Contracts.V1.Examples.Responses
{
	public class ListPostResponseExample : IExamplesProvider<List<Post>>
	{
		public List<Post> GetExamples()
		{
			var posts = new List<Post>();

			posts.Add(new Post
			{
				Id = Guid.NewGuid(),
				Text = "Some text",
				Created = DateTime.Now,
				CategoryId = Guid.NewGuid(),
				CreatedBy = Guid.NewGuid(),
			});

			posts.Add(new Post
			{
				Id = Guid.NewGuid(),
				Text = "Some text",
				Created = DateTime.Now,
				CategoryId = Guid.NewGuid(),
				CreatedBy = Guid.NewGuid(),
			});

			return posts;
		}
	}
}
