using System;

namespace ProjectSetup.Exceptions
{
	public class FastPostBadRequestException : Exception
	{
		public FastPostBadRequestException(string message) : base(message) { }
	}
}
