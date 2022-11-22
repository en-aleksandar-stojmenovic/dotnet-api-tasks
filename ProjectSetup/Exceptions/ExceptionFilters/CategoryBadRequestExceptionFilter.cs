﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectSetup.Domain;
using ProjectSetup.Services;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ProjectSetup.Exceptions.ExceptionFilters
{
	public class CategoryBadRequestExceptionFilter : ExceptionFilterAttribute
	{
		private readonly Type _exceptionType;
		private readonly HttpStatusCode _statusCode;

		public CategoryBadRequestExceptionFilter(Type exceptionType, HttpStatusCode httpStatusCode)
		{
			_exceptionType = exceptionType ?? throw new ArgumentNullException(nameof(exceptionType));

			_statusCode = httpStatusCode;
		}

		public override async Task OnExceptionAsync(ExceptionContext context)
		{
			var exception = context.Exception;

			//_logger.LogError($"Something went wrong: {exception}");

			if (exception.GetType() == _exceptionType)
			{
				context.HttpContext.Response.StatusCode = (int)_statusCode;
				context.HttpContext.Response.ContentType = "application/json";

				await context.HttpContext.Response.WriteAsync(new ErrorDetails()
				{
					StatusCode = context.HttpContext.Response.StatusCode,
					Message = exception.Message
				}.ToString());

				context.ExceptionHandled = true;
			}
		}
	}
}
