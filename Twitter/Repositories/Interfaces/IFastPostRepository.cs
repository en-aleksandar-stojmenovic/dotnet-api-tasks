using Twitter.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Twitter.Repositories.Interfaces
{
	public interface IFastPostRepository : IRepositoryBase<FastPost>
	{
		Task<FastPost> FindFastPostByIdAsync(Guid id);
		Task<List<FastPost>> FindAllFastPostsAsync();
		Task<FastPost> CreateFastPost(FastPost postRequest);
	}
}
