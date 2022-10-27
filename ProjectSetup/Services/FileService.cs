using ProjectSetup.Contracts.V1.Responses;
using System;

namespace ProjectSetup.Services
{
	public class FileService : IFileService
	{
		public ApiResponse LogErrorsAndReturnResponse(ApiResponse response)
		{
			LogErrorsInFile(response.StatusCode, response.Message);

			return new ApiResponse(response.StatusCode, response.Message);
		}

		public void LogErrorsInFile(int statusCode, string message)
		{
			string filePath = "log.txt";
			string log = DateTime.Now.ToString() + " [" + statusCode + "] " + message;
			if (System.IO.File.Exists(filePath))
			{
				string logs = System.IO.File.ReadAllText(filePath) + "\r\n" + log;
				System.IO.File.WriteAllText(filePath, logs);
			}
			else
			{
				System.IO.File.WriteAllText(filePath, log);
			}
		}
	}
}
