using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using Vidly.Api.Helpers;

namespace Vidly.Api.Filters;

public class AdminFilter : IAsyncActionFilter
{
	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		var jwt  = context.HttpContext.Items["user"]!.ToString();
		
		var jsonObject = JObject.Parse( (jwt!.Split('.')[^1]) );

		var isAdmin = bool.Parse(jsonObject["IsAdmin"]!.ToString());

		if (!isAdmin)
		{
			var filterProblemDetails = new FilterValidationProblemDetails
			{
				Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
				Title = "Authentication Error",
				Status = StatusCodes.Status400BadRequest,
				Instance = context.HttpContext.Request.Path
			};
			
			filterProblemDetails.Errors.Add("User", ["Access Denied"]);
			
			context.Result = filterProblemDetails.ObjectResult();
			
			return;
		}

		await next();

	}
}