using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using ProjectSetup.Contracts.V1.Requests;
using ProjectSetup.Contracts.V1.Responses;

namespace ProjectSetup.Repositories.Interfaces
{
	public interface IUserRepository : IRepositoryBase<IdentityUser>
    {
        Task<IdentityUser> FindUserByUsernameAsync(string username);
        Task<IdentityUser> CreateUser(RegisterUserRequest userRequest);
		Task<AuthResponse> Login(LoginUserRequest userRequest);
	}
}
