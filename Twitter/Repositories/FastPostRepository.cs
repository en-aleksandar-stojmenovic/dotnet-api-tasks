using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twitter.Data;
using Twitter.Domain;
using Twitter.Domain.Errors;
using Twitter.Exceptions;
using Twitter.Repositories.Interfaces;

namespace Twitter.Repositories
{
	public class FastPostRepository : RepositoryBase<FastPost>, IFastPostRepository
	{
		public FastPostRepository(ApplicationDbContext context) : base(context)
		{
		}

		public async Task<Result<FastPost>> CreateFastPost(FastPost postRequest)
		{
			if (await _context.Categories.FindAsync(postRequest.CategoryId) == null)
			{
				var result = Result.Fail(
					new Error("Cannot create fast post",
					new CategoryBadRequestError("Category with Id: " + postRequest.CategoryId + " doesn't exists."))
					).Log();

				return result;
			}

			postRequest.Id = Guid.NewGuid();
			postRequest.Created = DateTime.Now;

			Create(postRequest);

			return Result.Ok(postRequest);
		}

		public void DeleteFastPost(Guid id)
		{
			var post = FindByCondition(fastPost => fastPost.Id.Equals(id) && EF.Functions.DateDiffHour(fastPost.Created, DateTime.Now) < 24)
						.FirstOrDefaultAsync().Result;

			if (post == null)
			{
				throw new FastPostNotFoundException("FastPost with Id: '" + id + "' not found");
			}

			Delete(post);
		}

		public async Task<List<FastPost>> FindAllFastPostsAsync()
		{
			return await FindByCondition(fastPost => EF.Functions.DateDiffHour(fastPost.Created, DateTime.Now) < 24).ToListAsync();
		}

		public async Task<Result<FastPost>> FindFastPostByIdAsync(Guid id)
		{
			var fastPost = await FindByCondition(fastPost => fastPost.Id.Equals(id) && EF.Functions.DateDiffHour(fastPost.Created, DateTime.Now) < 24)
						.FirstOrDefaultAsync();

			if (fastPost == null)
			{
				var result = Result.Fail(new Error("Cannot find the fast post"))
				   .WithError(new FastPostNotFoundError("Fast post with id '" + id + "' is not available")).Log();

				return result;
			}

			return fastPost;
		}
	}
}
