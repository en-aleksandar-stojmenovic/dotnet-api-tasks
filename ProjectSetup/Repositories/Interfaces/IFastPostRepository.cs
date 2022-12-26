using ProjectSetup.Domain;
using System;
using System.Threading.Tasks;

namespace ProjectSetup.Repositories.Interfaces
{
	public interface IFastPostRepository : IRepositoryBase<FastPost>
	{
		Task<FastPost> FindFastPostByIdAsync(Guid id);
		Task<FastPost> CreateFastPost(FastPost postRequest);
	}
}
