using System.Collections.Generic;
using Twitter.Domain;

namespace Twitter.Contracts.V1.Responses
{
	public class AuthResponse
	{
		public bool Success { get; set; }
		public string Token { get; set; }
		public List<Post> Posts { get; set; }
	}
}
