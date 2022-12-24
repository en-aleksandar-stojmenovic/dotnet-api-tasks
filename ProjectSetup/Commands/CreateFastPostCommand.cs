using MediatR;
using ProjectSetup.Domain;
using System;

namespace ProjectSetup.Commands
{
	public class CreateFastPostCommand : IRequest<FastPost>
	{
		public string Text { get; set; }
		public Guid CategoryId { get; set; }
	}
}
