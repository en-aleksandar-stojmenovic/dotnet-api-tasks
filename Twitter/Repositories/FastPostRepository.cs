using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twitter.Data;
using Twitter.Domain;
using Twitter.Exceptions;
using Twitter.Repositories.Interfaces;

namespace Twitter.Repositories
{
	public class FastPostRepository : RepositoryBase<FastPost>, IFastPostRepository
	{
		public FastPostRepository(ApplicationDbContext context) : base(context)
		{
		}

		public async Task<FastPost> CreateFastPost(FastPost postRequest)
		{
			if (await _context.Categories.FindAsync(postRequest.CategoryId) == null)
			{
				throw new CategoryBadRequestException("Category with Id: '" + postRequest.CategoryId + "' not found");
			}

			postRequest.Id = Guid.NewGuid();
			postRequest.Created = DateTime.Now;

			Create(postRequest);

			return postRequest;
		}

		public async Task<List<FastPost>> FindAllFastPostsAsync()
		{
			var fastPosts = await FindByCondition(fastPost => EF.Functions.DateDiffHour(fastPost.Created, DateTime.Now) < 24).ToListAsync();

			return fastPosts;
		}

		public async Task<FastPost> FindFastPostByIdAsync(Guid id)
		{
			var fastPost = await FindByCondition(fastPost => fastPost.Id.Equals(id) && EF.Functions.DateDiffHour(fastPost.Created, DateTime.Now) < 24)
						.FirstOrDefaultAsync();

			if (fastPost == null)
			{
				throw new FastPostNotFoundException("FastPost with Id: '" + id + "' not found");
			}

			return fastPost;
		}
	}
}
