using Swashbuckle.AspNetCore.Filters;
using System;
using Twitter.Commands;

namespace Twitter.Examples.Requests
{
	public class CreateFastPostCommandExample : IExamplesProvider<CreateFastPostCommand>
	{
		public CreateFastPostCommand GetExamples()
		{
			return new CreateFastPostCommand
			{
				Text = "Some text",
				CategoryId = Guid.NewGuid()
			};
		}
	}
}
