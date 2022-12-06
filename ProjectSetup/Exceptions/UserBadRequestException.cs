using System;

namespace ProjectSetup.Exceptions
{
	public class UserBadRequestException : Exception
	{
		public UserBadRequestException(string message) : base(message) { }
	}
}
