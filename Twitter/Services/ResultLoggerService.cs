using FluentResults;
using Microsoft.Extensions.Logging;

namespace Twitter.Services
{
	public class ResultLoggerService : IResultLogger
	{
		private readonly ILoggerManager _logger;

		public ResultLoggerService(ILoggerManager logger)
		{
			_logger = logger;
		}

		public void Log(string context, string content, ResultBase result, LogLevel logLevel)
		{
			LogErrors(result);
		}


		public void Log<TContext>(string content, ResultBase result, LogLevel logLevel)
		{
			LogErrors(result);
		}

		private void LogErrors(ResultBase result)
		{
			foreach (var error in result.Errors)
			{
				if (error.Reasons.Count == 0)
				{
					_logger.LogError(error.Message);
				}
				else
				{
					foreach (var causedByError in error.Reasons)
					{
						_logger.LogError(error.Message + ". Caused by ERROR: " + causedByError.Message);
					}
				}
			}
		}
	}
}
