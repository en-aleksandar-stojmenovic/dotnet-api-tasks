using Microsoft.AspNetCore.Identity;
using ProjectSetup.Contracts.V1.Requests;
using ProjectSetup.Contracts.V1.Responses;
using System.Threading.Tasks;

namespace ProjectSetup.Repositories.Interfaces
{
	public interface IUserRepository : IRepositoryBase<IdentityUser>
	{
		Task<IdentityUser> FindUserByUsernameAsync(string username);
		Task<IdentityUser> CreateUser(RegisterUserRequest userRequest);
		Task<AuthResponse> Login(LoginUserRequest userRequest);
	}
}
