using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Vidly.Api.Helpers;
using Vidly.Contracts.Responses;

namespace Vidly.Api.Filters;

public class AuthFilter(IConfigurationManager config) : IAsyncActionFilter
{
	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		if (!context.HttpContext.Request.Headers.TryGetValue("x-auth-token", out var token))
		{

			var filterProblemDetails = new FilterValidationProblemDetails
			{
				Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
				Title = "Authentication Error",
				Status = StatusCodes.Status401Unauthorized,
				Instance = context.HttpContext.Request.Path
			};
			
			filterProblemDetails.Errors.Add("Auth Token", ["Token is required"]);
			
			context.Result = filterProblemDetails.ObjectResult();
			
			return;
		}
		
		var tokenHandler = new JwtSecurityTokenHandler();
		
		var key = Encoding.UTF8.GetBytes(config["JwtTokenSecret"]!);
		
		var validationParameters = new TokenValidationParameters
		{
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(key)
		};

		try
		{
			var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

			context.HttpContext.User = principal;
			context.HttpContext.Items["user"] = validatedToken;

		}
		catch (Exception ex)
		{
			
			var filterProblemDetails = new FilterValidationProblemDetails
			{
				Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
				Title = "Authentication Error",
				Status = StatusCodes.Status400BadRequest,
				Instance = context.HttpContext.Request.Path
			};
			
			filterProblemDetails.Errors.Add("Auth Token", [ex.Message]);
			
			context.Result = filterProblemDetails.ObjectResult();
			
			return;
		}
		
		await next();
	}
}