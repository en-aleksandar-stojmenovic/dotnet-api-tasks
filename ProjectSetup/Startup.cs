using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NLog;
using ProjectSetup.Data;
using ProjectSetup.Exceptions.ExceptionFilters;
using ProjectSetup.Middleware;
using ProjectSetup.Options;
using ProjectSetup.Services;
using System;
using System.IO;

namespace ProjectSetup
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));

			services.AddDatabaseDeveloperPageExceptionFilter();
			services.AddSingleton<ILoggerManager, LoggerManager>();
			services.AddControllers();
			services.AddScoped<CategoryExceptionFilter>();
			services.AddSwaggerGen(x =>
			{
				x.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseMiddleware<ExceptionMiddleware>();

			if (env.IsDevelopment())
			{
				var swaggerOptions = new SwaggerOptions();
				Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
				app.UseSwagger(option =>
				{
					option.RouteTemplate = swaggerOptions.JsonRoute;
				});

				app.UseSwaggerUI(option =>
				{
					option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
				});
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
