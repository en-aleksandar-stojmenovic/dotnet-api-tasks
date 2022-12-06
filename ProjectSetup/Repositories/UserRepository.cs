using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectSetup.Contracts.V1.Requests;
using ProjectSetup.Data;
using ProjectSetup.Domain;
using ProjectSetup.Exceptions;
using ProjectSetup.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProjectSetup.Repositories
{
    public class UserRepository : RepositoryBase<IdentityUser>, IUserRepository
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserRepository(ApplicationDbContext context,
			UserManager<IdentityUser> userManager,
			RoleManager<IdentityRole> roleManager) : base(context)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task<IdentityUser> CreateUser(RegisterUserRequest userRequest)
		{
			var userExists = await _userManager.FindByNameAsync(userRequest.Username);

			if (userExists != null)
				throw new UserBadRequestException("User with Username: '" + userRequest.Username + "' already exists.");

			IdentityUser user = new()
			{
				Email = userRequest.Email,
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = userRequest.Username
			};

			await _userManager.CreateAsync(user, userRequest.Password);

			await _userManager.AddToRoleAsync(user, UserRoles.User);

			return user;
		}

		public async Task<IdentityUser> FindUserByUsernameAsync(string username)
		{
			var user = await FindByCondition(post => post.UserName.Equals(username))
						.FirstOrDefaultAsync();

			if (user == null)
			{
				throw new UserNotFoundException("user with Username: '" + username + "' not found");
			}

			return user;
		}
	}
}
