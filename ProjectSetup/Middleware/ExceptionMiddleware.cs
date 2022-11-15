using Microsoft.AspNetCore.Http;
using ProjectSetup.Domain;
using ProjectSetup.Services;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ProjectSetup.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILoggerManager _logger;

		public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
		{
			_logger = logger;
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong: {ex}");
				await HandleExceptionAsync(context);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			await context.Response.WriteAsync(new ErrorDetails()
			{
				StatusCode = context.Response.StatusCode,
				Message = "Internal Server Error from the custom middleware."
			}.ToString());
		}
	}
}
