using FluentResults;

namespace Twitter.Domain.Errors
{
	public class CategoryBadRequestError : Error
	{
		public CategoryBadRequestError(string message) : base(message)
		{
			Metadata.Add("ErrorCode", "400");
		}
	}
}
