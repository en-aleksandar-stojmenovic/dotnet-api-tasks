namespace ProjectSetup.Contracts.V1.Responses
{
	public class ApiResponse
	{
		public ApiResponse(int statusCode, string message = null)
		{
			StatusCode = statusCode;
			Message = message ?? GetDefaultMessageForStatusCode(statusCode);
		}

		public int StatusCode { get; set; }
		public string Message { get; set; }

		private string GetDefaultMessageForStatusCode(int statusCode)
		{
			return statusCode switch
			{
				400 => "Bad Request",
				404 => "Not found",
				500 => "Server Error",
				_ => null
			};
		}

	}
}
