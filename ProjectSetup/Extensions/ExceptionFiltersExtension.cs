using Microsoft.Extensions.DependencyInjection;
using ProjectSetup.Exceptions.ExceptionFilters;

namespace ProjectSetup.Extensions
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
