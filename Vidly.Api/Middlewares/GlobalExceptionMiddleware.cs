using Microsoft.AspNetCore.Mvc;

namespace Vidly.Api.Middlewares;

public class GlobalExceptionMiddleware(RequestDelegate next)
{
	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await next(context);
		} 
		catch (Exception ex)
		{
			context.Response.StatusCode = StatusCodes.Status400BadRequest;
			context.Response.ContentType = "application/problem+json";
			
			var problemDetails = new ProblemDetails
			{
				Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
				Title = "An unexpected error occurred",
				Detail = ex.Message,
				Instance = context.Request.Path,
				Status = context.Response.StatusCode,
			};
			
			await context.Response.WriteAsJsonAsync(problemDetails);
			
		}
	}
}