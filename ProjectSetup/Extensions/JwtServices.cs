using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProjectSetup.Options;
using System.Text;

namespace ProjectSetup.Extensions
{
	public static class JwtServices
	{
		public static IServiceCollection AddJwtSettings(this IServiceCollection services, IConfiguration configuration)
		{
			var jwtSettings = new JwtSettings();
			configuration.Bind(nameof(jwtSettings), jwtSettings);
			services.AddSingleton(jwtSettings);

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
					ValidateIssuer = false,
					ValidateAudience = false,
					RequireExpirationTime = false,
					ValidateLifetime = true,
				};
			});

			return services;
		}
	}
}
