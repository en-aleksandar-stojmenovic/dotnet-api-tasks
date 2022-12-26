using System;

namespace ProjectSetup.Exceptions
{
	public class FastPostNotFoundException : Exception
	{
		public FastPostNotFoundException(string message) : base(message) { }
	}
}
