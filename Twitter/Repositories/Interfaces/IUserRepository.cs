using Microsoft.AspNetCore.Identity;
using Twitter.Contracts.V1.Requests;
using Twitter.Contracts.V1.Responses;
using System.Threading.Tasks;

namespace Twitter.Repositories.Interfaces
{
	public interface IUserRepository : IRepositoryBase<IdentityUser>
	{
		Task<IdentityUser> FindUserByUsernameAsync(string username);
		Task<IdentityUser> CreateUser(RegisterUserRequest userRequest);
		Task<AuthResponse> Login(LoginUserRequest userRequest);
	}
}
