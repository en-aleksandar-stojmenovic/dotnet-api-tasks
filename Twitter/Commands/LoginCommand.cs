using MediatR;
using Twitter.Contracts.V1.Responses;

namespace Twitter.Commands
{
	public class LoginCommand : IRequest<AuthResponse>
	{
		public string Username { get; set; }

		public string Password { get; set; }
	}
}
