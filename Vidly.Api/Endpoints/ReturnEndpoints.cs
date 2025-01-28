using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vidly.Api.Mappers;
using Vidly.Application.Repositories;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;

namespace Vidly.Api.Endpoints;

public class ReturnEndpoints : IEndpoints
{
	private const string BaseRoute = "returns";
	private const string Tag = "Return";

	public static void DefineEndpoints(IEndpointRouteBuilder app)
	{
		var group = app.BaseGroup().MapGroup(BaseRoute);

		group.MapPost("/", Create)
			.Produces<RentalResponse>(201)
			.Produces<ValidationProblemDetails>(400)
			.WithMethodName(Tag);
		
	}

	private static async Task<IResult> Create(
		[FromBody] ReturnRequest request,
		IReturnRepository returnRepository,
		CancellationToken token)
	{
		var rentalReturn = ReturnMapper.MapToReturn(request);

		var rental = await returnRepository.ReturnAsync(rentalReturn, token);

		var response = RentalMapper.MapToResponse(rental!);
		
		return Results.Ok(response);
	}
	
}