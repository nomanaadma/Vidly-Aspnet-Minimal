using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vidly.Application.Data;
using Vidly.Application.Health;
using Vidly.Application.Models;
using Vidly.Application.Repositories;
using Vidly.Application.Services;

namespace Vidly.Application;

public static class ApplicationServiceCollectionExtensions
{
	public static IServiceCollection AddApplication(this IServiceCollection services, IConfigurationManager config)
	{
		services.AddScoped<IGenreRepository, GenreRepository>();
		services.AddScoped<ICustomerRepository, CustomerRepository>();
		services.AddScoped<IMovieRepository, MovieRepository>();
		services.AddScoped<IRentalRepository, RentalRepository>();
		services.AddScoped<IReturnRepository, ReturnRepository>();
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<IUserService, UserService>();
		
		services.AddHealthChecks()
			.AddCheck<DatabaseHealthCheck>(DatabaseHealthCheck.Name);
		
		services.AddSingleton(config);
		
		services.AddValidatorsFromAssemblyContaining<IApplicationMarker>();
		
		return services;
	}
	
	public static IServiceCollection AddDatabase(this IServiceCollection services, 
		string? connectionString)
	{
		services.AddDbContext<DatabaseContext>(optionBuilder =>
		{
			optionBuilder.UseNpgsql(connectionString);
			optionBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
		});
		
		services.AddScoped<DbInitializer>();
		
		return services;
	}
	
}