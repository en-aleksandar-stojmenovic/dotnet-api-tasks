using System.Threading.Tasks;

namespace ProjectSetup.Contracts.V1
{
	public interface IRepositoryWrapper
	{
		IPostRepository Post { get; }
		IUserRepository User { get; }
		Task<int> SaveAsync();
	}
}
