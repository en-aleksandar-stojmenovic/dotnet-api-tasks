using Twitter.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Twitter.Repositories.Interfaces
{
	public interface IPostRepository : IRepositoryBase<Post>
	{
		Task<Post> FindPostByIdAsync(Guid id);
		Task<IEnumerable<Post>> FindAllPostsAsync();
		Task<Post> CreatePost(Post postRequest);
		Task<Post> UpdatePost(Post postRequest);
		void DeletePost(Guid id);
		Task<bool> UserOwnsPostAsync(Guid postId, Guid userId);
	}
}
