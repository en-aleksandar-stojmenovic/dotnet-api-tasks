using System;

namespace ProjectSetup.Exceptions
{
	public class CategoryBadRequestException: Exception
	{
		public CategoryBadRequestException(string message) : base(message) { }
	}
}
