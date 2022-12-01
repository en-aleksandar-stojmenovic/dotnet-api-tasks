using System;

namespace ProjectSetup.Exceptions
{
	public class UserNotFoundException : Exception
	{
		public UserNotFoundException(string message) : base(message) { }
	}
}
