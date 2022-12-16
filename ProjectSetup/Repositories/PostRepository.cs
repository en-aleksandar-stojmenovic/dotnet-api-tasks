using Microsoft.EntityFrameworkCore;
using ProjectSetup.Data;
using ProjectSetup.Domain;
using ProjectSetup.Exceptions;
using ProjectSetup.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectSetup.Repositories
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
			var post = await FindByCondition(post => post.Id.Equals(id))
						.FirstOrDefaultAsync();

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
			var post = FindByCondition(post => post.Id.Equals(postId))
						.FirstOrDefaultAsync().Result;

			if (post == null)
			{
				throw new PostBadRequestException("Post with Id: '" + postId + "' not found");
			}

			Delete(post);
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
	}
}
