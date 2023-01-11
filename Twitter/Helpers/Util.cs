using FluentResults;
using System;
using Twitter.Domain;

namespace Twitter.Helpers
{
	public static class Util<T>
	{
		public static ErrorDetails ReadErrors(Result<T> result)
		{
			var errorDetails = new ErrorDetails();

			foreach (var error in result.Errors)
			{
				if (error.Reasons.Count == 0)
				{
					errorDetails.Message += "ERROR: " + error.Message + ". ";

					if (errorDetails.StatusCode == 0)
					{
						errorDetails.StatusCode = Int32.Parse(error.Metadata.Values.ToString());
					}
				}
				else
				{
					foreach (var causedByError in error.Reasons)
					{
						errorDetails.Message += "ERROR: " + error.Message + ". Caused by ERROR: " + causedByError.Message;

						if (errorDetails.StatusCode == 0)
						{
							errorDetails.StatusCode = Int32.Parse(causedByError.Metadata["ErrorCode"].ToString());
						}
					}
				}
			}

			return errorDetails;
		}
	}
}
