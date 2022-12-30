using Swashbuckle.AspNetCore.Filters;
using System;
using Twitter.Domain;

namespace Twitter.Contracts.V1.Examples.Responses
{
	public class PostResponseExample : IExamplesProvider<Post>
	{
		public Post GetExamples()
		{
			return new Post
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

