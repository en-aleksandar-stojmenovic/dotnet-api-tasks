﻿using System;

namespace Twitter.Exceptions
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException(string message) : base(message) { }
    }
}