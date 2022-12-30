using Swashbuckle.AspNetCore.Filters;
using System;
using Twitter.Contracts.V1.Requests;

namespace Twitter.Contracts.V1.Examples.Requests
{
	public class UpdatePostRequestExample : IExamplesProvider<UpdatePostRequest>
	{
		public UpdatePostRequest GetExamples()
		{
			return new UpdatePostRequest
			{
				Id = Guid.NewGuid(),
				Text = "Some text",
				CategoryId = Guid.NewGuid()
			};
		}
	}
}
