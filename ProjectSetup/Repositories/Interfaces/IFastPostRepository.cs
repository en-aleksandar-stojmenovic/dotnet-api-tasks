using ProjectSetup.Domain;
using System.Threading.Tasks;

namespace ProjectSetup.Repositories.Interfaces
{
	public interface IFastPostRepository : IRepositoryBase<FastPost>
	{
		Task<FastPost> CreateFastPost(FastPost postRequest);
	}
}
