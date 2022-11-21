using System.Threading.Tasks;

namespace ProjectSetup.Contracts.V1
{
	interface IRepositoryWrapper
	{
		IPostRepository Post { get; }
		Task SaveAsync();
	}
}
