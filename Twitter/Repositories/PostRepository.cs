using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twitter.Data;
using Twitter.Domain;
using Twitter.Exceptions;
using Twitter.Extensions;
using Twitter.Repositories.Interfaces;

namespace Twitter.Repositories
{
	public class PostRepository : RepositoryBase<Post>, IPostRepository
	{
		public PostRepository(ApplicationDbContext context)
			: base(context)
		{
		}

		public async Task<IEnumerable<Post>> FindAllPostsAsync()
		{
			return await FindAll().ToListAsync();
		}

		public async Task<Post> FindPostByIdAsync(Guid id)
		{
			var post = await FindByCondition(post => post.Id.Equals(id) && post.IsArchived == false)
						.SingleOrDefaultAsync();

			if (post == null)
			{
				throw new PostNotFoundException("Post with Id: '" + id + "' not found");
			}

			return post;
		}

		public async Task<Post> CreatePost(Post post)
		{
			if (await _context.Categories.FindAsync(post.CategoryId) == null)
			{
				throw new CategoryBadRequestException("Category with Id: '" + post.CategoryId + "' not found");
			}

			post.Id = Guid.NewGuid();
			post.Created = DateTime.Now;

			Create(post);

			return post;
		}

		public async Task<Post> UpdatePost(Post postRequest)
		{
			if (await _context.Categories.FindAsync(postRequest.CategoryId) == null)
			{
				throw new CategoryBadRequestException("Category with Id: '" + postRequest.CategoryId + "' not found");
			}

			Update(postRequest);

			return postRequest;
		}
		public void DeletePost(Guid postId)
		{
			var post = FindByCondition(post => post.Id.Equals(postId) && post.IsArchived == false)
						.FirstOrDefaultAsync().Result;

			if (post == null)
			{
				throw new PostBadRequestException("Post with Id: '" + postId + "' not found");
			}

			post.IsArchived = true;

			Update(post);
		}

		public async Task<bool> UserOwnsPostAsync(Guid postId, Guid userId)
		{
			var post = await _context.Posts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == postId);

			if (post == null)
			{
				return false;
			}

			if (post.CreatedBy != userId)
			{
				return false;
			}

			return true;
		}

		public async Task<int> NumberOfAvailablePosts(Guid? categoryId)
		{
			if (categoryId == null)
				return await FindByCondition(post => post.IsArchived == false).CountAsync();

			return await FindByCondition(post => post.CategoryId.Equals(categoryId) && post.IsArchived == false).CountAsync();
		}

		public async Task<List<Post>> FindUserPostsAsync(Guid userId)
		{
			var firstPost = await FindByCondition(post =>
						post.CreatedBy.Equals(userId) && post.IsArchived == false).OrderBy(post => post.Created).FirstOrDefaultAsync();

			var thirdPost = await FindByCondition(post =>
						post.CreatedBy.Equals(userId) && post.IsArchived == false).OrderBy(post => post.Created).Skip(2).Take(1).FirstOrDefaultAsync();

			var lastPost = await FindByCondition(post =>
						post.CreatedBy.Equals(userId) && post.IsArchived == false).OrderBy(post => post.Created).LastOrDefaultAsync();

			List<Post> userPosts = new List<Post>();

			userPosts.AddIfNotExists(lastPost);
			userPosts.AddIfNotExists(thirdPost);
			userPosts.AddIfNotExists(firstPost);

			return userPosts;
		}

		public async Task<List<Post>> ReadAllPostsAsync(PaginationFilter paginationFilter)
		{
			if (paginationFilter.CategoryId is null)
			{
				return await FindAll().ToListAsync();
			}

			var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
			return await FindByCondition(post =>
						post.CategoryId.Equals(paginationFilter.CategoryId) && post.IsArchived == false)
						.Skip(skip).Take(paginationFilter.PageSize).ToListAsync();
		}
	}
}
