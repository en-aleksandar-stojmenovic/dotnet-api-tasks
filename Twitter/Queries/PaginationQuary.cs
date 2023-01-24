using System;

namespace Twitter.Queries
{
	public class PaginationQuary
	{
		public PaginationQuary()
		{
			PageNumber = 1;
			PageSize = 100;
			CategoryId = null;
		}

		public PaginationQuary(int pageNumber, int pageSize, Guid categoryId)
		{
			PageNumber = pageNumber;
			PageSize = pageSize;
			CategoryId = categoryId;
		}

		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public Guid? CategoryId { get; set; }
	}
}
