using Microsoft.Extensions.DependencyInjection;
using Twitter.Exceptions.ExceptionFilters;

namespace Twitter.Extensions
{
	public static class ExceptionFiltersExtension
	{
		public static IServiceCollection AddExceptionFilters(this IServiceCollection services)
		{
			services.AddScoped<CustomExceptionFilter>();

			return services;
		}
	}
}
