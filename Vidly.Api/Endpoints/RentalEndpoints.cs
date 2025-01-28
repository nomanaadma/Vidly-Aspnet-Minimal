using Microsoft.AspNetCore.Mvc;
using Vidly.Api.Mappers;
using Vidly.Application.Repositories;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;

namespace Vidly.Api.Endpoints;

public class RentalEndpoints : IEndpoints
{
	private const string BaseRoute = "rentals";
	private const string Tag = "Rental";

	public static void DefineEndpoints(IEndpointRouteBuilder app)
	{
		var group = app.BaseGroup().MapGroup(BaseRoute);

		group.MapPost("/", Create)
			.Produces<RentalResponse>(201)
			.Produces<ValidationProblemDetails>(400)
			.WithMethodName(Tag);

		group.MapGet("/{id:int}", Get)
			.Produces<RentalResponse>(200)
			.Produces(404)
			.WithMethodName(Tag);

		group.MapGet("/", GetAll)
			.Produces<RentalsResponse>(200)
			.WithMethodName(Tag);
		
	}

	private static async Task<IResult> Create(
		[FromBody] RentalRequest request,
		IRentalRepository rentalRepository,
		CancellationToken token)
	{
		var rental = RentalMapper.MapToRental(request);

		rental = await rentalRepository.CreateAsync(rental, token);

		var response = RentalMapper.MapToResponse(rental);
		return Results.CreatedAtRoute($"Get{Tag}", new { id = response.Id }, response);
	}

	private static async Task<IResult> Get(
		[FromRoute] int id,
		IRentalRepository rentalRepository,
		CancellationToken token)
	{
		var rental = await rentalRepository.GetByIdAsync(id, token);

		if (rental is null)
			return Results.NotFound();

		var response = RentalMapper.MapToResponse(rental);
		return Results.Ok(response);
	}

	private static async Task<IResult> GetAll(
		IRentalRepository rentalRepository,
		CancellationToken token)
	{
		var rentals = await rentalRepository.GetAllAsync(token);
		var response = RentalMapper.MapToListResponse(rentals);
		return Results.Ok(response);
	}
	
}