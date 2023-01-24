using System;

namespace Twitter.Domain
{
	public class PaginationFilter
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public Guid? CategoryId { get; set; }
	}
}
