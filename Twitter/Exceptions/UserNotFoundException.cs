using System;

namespace Twitter.Exceptions
{
	public class UserNotFoundException : Exception
	{
		public UserNotFoundException(string message) : base(message) { }
	}
}
