using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Vidly.Api.Mappers;

namespace Vidly.Api.Middlewares;

public class ValidationMiddleware(RequestDelegate next)
{
	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await next(context);
		} 
		catch (ValidationException ex)
		{
			context.Response.StatusCode = StatusCodes.Status400BadRequest;
			context.Response.ContentType = "application/problem+json";
			
			var problemDetails = new ValidationProblemDetails
			{
				Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
				Title = "One or more validation errors occurred.",
				Status = context.Response.StatusCode,
			};

			foreach (var error in ex.Errors)
				if(error.PropertyName is not null)
					problemDetails.Errors[error.PropertyName] = [error.ErrorMessage];

			await context.Response.WriteAsJsonAsync(problemDetails);
			
		}
	}
}