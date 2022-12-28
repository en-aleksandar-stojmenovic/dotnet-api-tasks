using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twitter.Domain;

namespace Twitter.Repositories.Interfaces
{
	public interface IFastPostRepository : IRepositoryBase<FastPost>
	{
		Task<FastPost> FindFastPostByIdAsync(Guid id);
		Task<List<FastPost>> FindAllFastPostsAsync();
		void DeleteFastPost(Guid id);
		Task<FastPost> CreateFastPost(FastPost postRequest);
	}
}
