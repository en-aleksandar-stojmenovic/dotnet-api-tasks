using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using ProjectSetup.Contracts.V1.Requests;

namespace ProjectSetup.Contracts.V1
{
	public interface IUserRepository: IRepositoryBase<IdentityUser>
	{
		Task<IdentityUser> FindUserByUsernameAsync(string username);
		Task<IdentityUser> CreateUser(RegisterUserRequest userRequest);
	}
}
