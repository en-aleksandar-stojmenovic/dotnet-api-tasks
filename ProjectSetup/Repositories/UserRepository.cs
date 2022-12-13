using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectSetup.Contracts.V1.Requests;
using ProjectSetup.Contracts.V1.Responses;
using ProjectSetup.Data;
using ProjectSetup.Domain;
using ProjectSetup.Exceptions;
using ProjectSetup.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSetup.Repositories
{
	public class UserRepository : RepositoryBase<IdentityUser>, IUserRepository
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IConfiguration _config;

		public UserRepository(ApplicationDbContext context,
			UserManager<IdentityUser> userManager,
			RoleManager<IdentityRole> roleManager,
			IConfiguration config) : base(context)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_config = config;
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

		public async Task<AuthResponse> Login(LoginUserRequest userRequest)
		{
			var user = await _userManager.FindByNameAsync(userRequest.Username);
			if (user != null && await _userManager.CheckPasswordAsync(user, userRequest.Password))
			{
				var userRoles = await _userManager.GetRolesAsync(user);

				var authClaims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, user.UserName),
					new Claim("id", user.Id)
				};

				foreach (var userRole in userRoles)
				{
					authClaims.Add(new Claim(ClaimTypes.Role, userRole));
				}

				var token = GetToken(authClaims);

				return new AuthResponse
				{
					Success = true,
					Token = new JwtSecurityTokenHandler().WriteToken(token)
				};
			}
			else
			{
				throw new UserBadRequestException("Username or password is not correct!");
			}
		}

		private JwtSecurityToken GetToken(List<Claim> authClaims)
		{
			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Secret"]));

			var token = new JwtSecurityToken(
				expires: DateTime.Now.AddHours(3),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
				);

			return token;
		}
	}
}
