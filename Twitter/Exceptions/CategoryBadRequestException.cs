using System;

namespace Twitter.Exceptions
{
    public class CategoryBadRequestException : Exception
    {
        public CategoryBadRequestException(string message) : base(message) { }
    }
}
