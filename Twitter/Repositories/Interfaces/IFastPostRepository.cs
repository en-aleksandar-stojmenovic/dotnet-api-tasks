using FluentResults;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twitter.Domain;

namespace Twitter.Repositories.Interfaces
{
	public interface IFastPostRepository : IRepositoryBase<FastPost>
	{
		Task<Result<FastPost>> FindFastPostByIdAsync(Guid id);
		Task<List<FastPost>> FindAllFastPostsAsync();
		void DeleteFastPost(Guid id);
		Task<Result<FastPost>> CreateFastPost(FastPost postRequest);
	}
}
