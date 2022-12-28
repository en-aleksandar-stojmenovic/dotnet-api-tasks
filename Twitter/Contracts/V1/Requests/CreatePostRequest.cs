using System;

namespace Twitter.Contracts.V1.Requests
{
	public class CreatePostRequest
	{
		public string Text { get; set; }
		public Guid CategoryId { get; set; }
	}
}
