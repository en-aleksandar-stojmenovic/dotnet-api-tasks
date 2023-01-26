using System.Collections.Generic;

namespace Twitter.Contracts.V1.Responses
{
	public class PagedResponse<T>
	{
		public PagedResponse() { }

		public PagedResponse(IEnumerable<T> response)
		{
			Data = response;
		}

		public IEnumerable<T> Data { get; set; }
		public int? PageNumber { get; set; }
		public int? PageSize { get; set; }
	}
}
