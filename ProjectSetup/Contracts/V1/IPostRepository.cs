using ProjectSetup.Contracts.V1.Requests;
using ProjectSetup.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectSetup.Contracts.V1
{
	public interface IPostRepository : IRepositoryBase<Post>
	{
		Task<Post> FindPostByIdAsync(Guid id);
		Task<IEnumerable<Post>> FindAllPostsAsync();
		void CreatePost(Post post);
		Task<Post> UpdatePost(UpdatePostRequest post);
		void DeletePost(Post post);
	}
}
