using Microsoft.AspNetCore.Mvc;
using Vidly.Api.Mappers;
using Vidly.Application.Repositories;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;

namespace Vidly.Api.Endpoints;

public class GenreEndpoints : IEndpoints
{
	private const string BaseRoute = "genres";
	private const string Tag = "Genre";

	public static void DefineEndpoints(IEndpointRouteBuilder app)
	{
		var group = app.BaseGroup().MapGroup(BaseRoute);

		group.MapPost("/", Create)
			.Produces<GenreResponse>(201)
			.Produces<ValidationProblemDetails>(400)
			.WithMethodName(Tag);

		group.MapGet("/{id:int}", Get)
			.Produces<GenreResponse>(200)
			.Produces(404)
			.WithMethodName(Tag);

		group.MapGet("/", GetAll)
			.Produces<GenresResponse>(200)
			.WithMethodName(Tag);

		group.MapPut("/{id:int}", Update)
			.Produces<GenreResponse>(200)
			.Produces<ValidationProblemDetails>(400)
			.Produces(404)
			.WithMethodName(Tag);

		group.MapDelete("/{id:int}", Delete)
			.Produces(204)
			.Produces(404)
			.WithMethodName(Tag);
		
	}

	private static async Task<IResult> Create(
		[FromBody] GenreRequest request,
		IGenreRepository genreRepository,
		CancellationToken token)
	{
		var genre = GenreMapper.MapToGenre(request);

		genre = await genreRepository.CreateAsync(genre, token);

		var response = GenreMapper.MapToResponse(genre);
		return Results.CreatedAtRoute($"Get{Tag}", new { id = response.Id }, response);
	}

	private static async Task<IResult> Get(
		[FromRoute] int id,
		IGenreRepository genreRepository,
		CancellationToken token)
	{
		var genre = await genreRepository.GetByIdAsync(id, token);

		if (genre is null)
			return Results.NotFound();

		var response = GenreMapper.MapToResponse(genre);
		return Results.Ok(response);
	}

	private static async Task<IResult> GetAll(
		IGenreRepository genreRepository,
		CancellationToken token)
	{
		var genres = await genreRepository.GetAllAsync(token);
		var response = GenreMapper.MapToListResponse(genres);
		return Results.Ok(response);
	}

	private static async Task<IResult> Update(
		[FromRoute] int id,
		[FromBody] GenreRequest request,
		IGenreRepository genreRepository,
		CancellationToken token)
	{
		var existingGenre = await genreRepository.GetByIdAsync(id, token);

		if (existingGenre is null)
			return Results.NotFound();

		var genre = GenreMapper.MapToGenreWithId(request, id);

		genre = await genreRepository.UpdateAsync(genre, token);

		var response = GenreMapper.MapToResponse(genre);

		return Results.Ok(response);
	}

	private static async Task<IResult> Delete(
		[FromRoute] int id,
		IGenreRepository genreRepository,
		CancellationToken token)
	{
		var existingGenre = await genreRepository.GetByIdAsync(id, token);

		if (existingGenre is null)
			return Results.NotFound();

		await genreRepository.DeleteByIdAsync(existingGenre, token);

		return Results.NoContent();
	}
	
}