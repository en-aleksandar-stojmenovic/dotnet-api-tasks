using Microsoft.EntityFrameworkCore;
using ProjectSetup.Contracts.V1;
using ProjectSetup.Data;
using ProjectSetup.Domain;
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
			return await FindByCondition(post => post.Id.Equals(id))
						.FirstOrDefaultAsync();
		}

		public void CreatePost(Post post)
		{
			Create(post);
		}

		public void UpdatePost(Post post)
		{
			Update(post);
		}

		public void DeletePost(Post post)
		{
			Delete(post);
		}
	}
}
