using Swashbuckle.AspNetCore.Filters;
using System;
using Twitter.Contracts.V1.Requests;

namespace Twitter.Contracts.V1.Examples.Requests
{
	public class CreatePostRequestExample : IExamplesProvider<CreatePostRequest>
	{
		public CreatePostRequest GetExamples()
		{
			return new CreatePostRequest
			{
				Text = "Come text",
				CategoryId = Guid.NewGuid()
			};
		}
	}
}
