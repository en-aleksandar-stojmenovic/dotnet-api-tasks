using Microsoft.EntityFrameworkCore;
using ProjectSetup.Contracts.V1;
using ProjectSetup.Contracts.V1.Requests;
using ProjectSetup.Data;
using ProjectSetup.Domain;
using ProjectSetup.Exceptions;
using ProjectSetup.Exceptions.ExceptionFilters;
using System;
using System.Collections.Generic;
using System.Net;
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
			return await FindByCondition(post => post.Id.Equals(id))
						.FirstOrDefaultAsync();
		}

		public void CreatePost(Post post)
		{
			Create(post);
		}

		public async Task<Post> UpdatePost(UpdatePostRequest postRequest)
		{
			var post = await FindPostByIdAsync(postRequest.Id);

			if (post == null)
			{
				throw new PostBadRequestException("Post with Id: '" + postRequest.Id + "' not found");
			}

			if (await _context.Categories.FindAsync(postRequest.CategoryId) == null)
			{
				throw new CategoryBadRequestException("Category with Id: '" + postRequest.CategoryId + "' not found");
			}

			post.CategoryId = postRequest.CategoryId;
			post.Text = postRequest.Text;

			Update(post);

			return post;
		}

		public void DeletePost(Post post)
		{
			Delete(post);
		}
	}
}
