using ProjectSetup.Contracts.V1.Responses;

namespace ProjectSetup.Services
{
	public interface IFileService
	{
		void LogErrorsInFile(int statusCode, string message);
		ApiResponse LogErrorsAndReturnResponse(ApiResponse response);
	}
}
