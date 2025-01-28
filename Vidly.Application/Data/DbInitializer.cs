using Microsoft.EntityFrameworkCore;

namespace Vidly.Application.Data;

public class DbInitializer(DatabaseContext context)
{
	public async Task InitializeAsync()
	{
		await context.Genres.FindAsync(1);
		
		var pendingMigration = await context.Database.GetPendingMigrationsAsync();
		if (pendingMigration.Any())
			throw new Exception("Database is not fully migrated");
		
	}
}