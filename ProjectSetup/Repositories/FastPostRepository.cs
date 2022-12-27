﻿using Microsoft.EntityFrameworkCore;
using ProjectSetup.Data;
using ProjectSetup.Domain;
using ProjectSetup.Exceptions;
using ProjectSetup.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectSetup.Repositories
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

			if (fastPosts.Count == 0)
			{
				throw new FastPostNotFoundException("FastPosts are no longer available");
			}

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
