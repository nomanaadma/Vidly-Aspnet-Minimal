using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Vidly.Application.Data;

namespace Vidly.Application.Health;

public class DatabaseHealthCheck(
	DatabaseContext context, ILogger<DatabaseHealthCheck> logger) : IHealthCheck
{
	public const string Name = "Database";

	public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext hcontext, 
		CancellationToken cancellationToken = new())
	{

		var canConnect = await context.Database.CanConnectAsync(cancellationToken);

		if (canConnect) return HealthCheckResult.Healthy();
		
		const string errorMessage = "Database is unhealthy";
		logger.LogError(errorMessage);
		return HealthCheckResult.Unhealthy(errorMessage);

	}
}