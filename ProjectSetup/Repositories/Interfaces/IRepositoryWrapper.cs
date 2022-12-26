using System.Threading.Tasks;

namespace ProjectSetup.Repositories.Interfaces
{
	public interface IRepositoryWrapper
	{
		IPostRepository Post { get; }
		IFastPostRepository FastPost { get; }
		IUserRepository User { get; }
		Task<int> SaveAsync();
	}
}
