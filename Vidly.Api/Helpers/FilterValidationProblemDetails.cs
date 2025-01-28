using Microsoft.AspNetCore.Mvc;

namespace Vidly.Api.Helpers;

public class FilterValidationProblemDetails : ValidationProblemDetails
{
	public IActionResult ObjectResult()
	{
		return new ObjectResult(this)
		{
			StatusCode = this.Status,
			ContentTypes = ["application/problem+json"]
		};
	}
	
}