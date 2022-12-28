using System;

namespace Twitter.Contracts.V1.Requests
{
	public class UpdatePostRequest
	{
		public Guid Id { get; set; }
		public string Text { get; set; }
		public Guid CategoryId { get; set; }
	}
}
