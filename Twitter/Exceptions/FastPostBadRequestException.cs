using System;

namespace Twitter.Exceptions
{
	public class FastPostBadRequestException : Exception
	{
		public FastPostBadRequestException(string message) : base(message) { }
	}
}
