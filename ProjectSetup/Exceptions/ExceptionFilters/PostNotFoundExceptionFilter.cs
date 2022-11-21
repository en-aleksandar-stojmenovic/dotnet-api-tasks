using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectSetup.Domain;
using ProjectSetup.Services;
using System.Net;
using System.Threading.Tasks;

namespace ProjectSetup.Exceptions.ExceptionFilters
{
	public class PostNotFoundExceptionFilter : ExceptionFilterAttribute
	{
		private readonly ILoggerManager _logger;

		public PostNotFoundExceptionFilter(ILoggerManager logger)
		{
			_logger = logger;
		}

		public override async Task OnExceptionAsync(ExceptionContext context)
		{
			var exceptionType = context.Exception;
			var exception = context.Exception.InnerException ?? context.Exception;

			_logger.LogError($"Something went wrong: {exceptionType}");

			context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
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
