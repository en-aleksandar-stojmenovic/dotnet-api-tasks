using System;

namespace Twitter.Exceptions
{
	public class UserBadRequestException : Exception
	{
		public UserBadRequestException(string message) : base(message) { }
	}
}
