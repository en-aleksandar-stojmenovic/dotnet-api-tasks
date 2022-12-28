using System;

namespace Twitter.Exceptions
{
    public class PostBadRequestException : Exception
    {
        public PostBadRequestException(string message) : base(message) { }
    }
}
