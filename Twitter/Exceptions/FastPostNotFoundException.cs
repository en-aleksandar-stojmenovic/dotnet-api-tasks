using System;

namespace Twitter.Exceptions
{
	public class FastPostNotFoundException : Exception
	{
		public FastPostNotFoundException(string message) : base(message) { }
	}
}
