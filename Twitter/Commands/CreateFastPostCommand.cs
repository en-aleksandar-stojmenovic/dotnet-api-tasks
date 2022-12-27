using MediatR;
using System;
using Twitter.Domain;

namespace Twitter.Commands
{
	public class CreateFastPostCommand : IRequest<FastPost>
	{
		public string Text { get; set; }
		public Guid CategoryId { get; set; }
	}
}
