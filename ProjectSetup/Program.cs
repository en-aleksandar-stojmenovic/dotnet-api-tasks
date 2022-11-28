using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using ProjectSetup.Contracts.V1;
using ProjectSetup.Data;
using ProjectSetup.Extensions;
using ProjectSetup.Middleware;
using ProjectSetup.Options;
using ProjectSetup.Repositories;
using ProjectSetup.Services;
using System;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.Configure<JsonOptions>(options =>
{
	options.SerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
			   options.UseSqlServer(
				   builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddControllers();
builder.Services.AddExceptionFilters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
	var swaggerOptions = new SwaggerOptions();
	builder.Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
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

app.MapControllers();

app.Run();