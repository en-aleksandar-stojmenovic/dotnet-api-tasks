using Microsoft.Extensions.DependencyInjection;
using ProjectSetup.Exceptions.ExceptionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSetup.Extensions
{
	public static class ExceptionFiltersExtension
	{
		public static IServiceCollection AddExceptionFilters(this IServiceCollection services)
		{
			services.AddScoped<CategoryNotFoundExceptionFilter>();
			services.AddScoped<CategoryBadRequestExceptionFilter>();
			services.AddScoped<PostNotFoundExceptionFilter>();
			services.AddScoped<PostBadRequestExceptionFilter>();

			return services;
		}
	}
}
