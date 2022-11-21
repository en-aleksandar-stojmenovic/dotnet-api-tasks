using System;

namespace ProjectSetup.Exceptions
{
	public class PostBadRequestException: Exception
	{
		public PostBadRequestException(string message) : base(message) { }
	}
}
