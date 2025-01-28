using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Vidly.Application.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
	public DatabaseContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

		var config = new ConfigurationBuilder()
			.SetBasePath( Path.Combine(Directory.GetCurrentDirectory(), "../Vidly.Api") )
			.AddJsonFile("appsettings.json")
			.Build();

		var connectionString = config.GetConnectionString("database") ??
		                       throw new InvalidOperationException("Connection string 'database' not found.");
			
		optionsBuilder.UseNpgsql(connectionString);

		return new DatabaseContext(optionsBuilder.Options);
	}
}