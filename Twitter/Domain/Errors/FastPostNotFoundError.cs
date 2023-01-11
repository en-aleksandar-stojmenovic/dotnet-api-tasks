using FluentResults;

namespace Twitter.Domain.Errors
{
	public class FastPostNotFoundError : Error
	{
		public FastPostNotFoundError(string message) : base(message)
		{
			Metadata.Add("ErrorCode", "404");
		}
	}
}
