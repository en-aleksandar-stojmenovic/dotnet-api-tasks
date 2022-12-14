using ProjectSetup.Contracts.V1.Requests;
using ProjectSetup.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectSetup.Repositories.Interfaces
{
	public interface IPostRepository : IRepositoryBase<Post>
	{
		Task<Post> FindPostByIdAsync(Guid id);
		Task<IEnumerable<Post>> FindAllPostsAsync();
		Task<Post> CreatePost(Post postRequest);
		Task<Post> UpdatePost(UpdatePostRequest postRequest);
		void DeletePost(Guid id);
		Task<bool> UserOwnsPostAsync(Guid id, Guid userId);
	}
}
