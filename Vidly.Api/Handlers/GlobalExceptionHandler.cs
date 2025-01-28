using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Vidly.Api.Handlers;

public class GlobalExceptionHandler : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(
		HttpContext httpContext,
		Exception exception,
		CancellationToken cancellationToken)
	{
				
		httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

		var problemDetails = new ProblemDetails
		{
			Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
			Title = "An unexpected error occurred",
			Detail = exception.Message,
			Instance = httpContext.Request.Path,
			Status = httpContext.Response.StatusCode,
		};

		await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

		return true;
	}
}