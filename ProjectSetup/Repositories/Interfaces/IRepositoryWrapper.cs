using System.Threading.Tasks;

namespace ProjectSetup.Repositories.Interfaces
{
	public interface IRepositoryWrapper
    {
        IPostRepository Post { get; }
        IUserRepository User { get; }
        Task<int> SaveAsync();
    }
}
