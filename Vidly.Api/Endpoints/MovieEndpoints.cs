using Microsoft.AspNetCore.Mvc;
using Vidly.Api.Endpoints.Internal;
using Vidly.Api.Mappers;
using Vidly.Application.Repositories;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;

namespace Vidly.Api.Endpoints;

public class MovieEndpoints : IEndpoints
{
	private const string BaseRoute = "movies";
	private const string Tag = "Movie";

	public static void DefineEndpoints(IEndpointRouteBuilder app)
	{
		var group = app.BaseGroup().MapGroup(BaseRoute);

		group.MapPost("/", Create)
			.Produces<MovieResponse>(201)
			.Produces<ValidationProblemDetails>(400)
			.WithMethodName(Tag);

		group.MapGet("/{id:int}", Get)
			.Produces<MovieResponse>(200)
			.Produces(404)
			.WithMethodName(Tag);

		group.MapGet("/", GetAll)
			.Produces<MoviesResponse>(200)
			.WithMethodName(Tag);

		group.MapPut("/{id:int}", Update)
			.Produces<MovieResponse>(200)
			.Produces<ValidationProblemDetails>(400)
			.Produces(404)
			.WithMethodName(Tag);

		group.MapDelete("/{id:int}", Delete)
			.Produces(204)
			.Produces(404)
			.WithMethodName(Tag);
		
	}

	private static async Task<IResult> Create(
		[FromBody] MovieRequest request,
		IMovieRepository movieRepository,
		CancellationToken token)
	{
		var movie = MovieMapper.MapToMovie(request);

		movie = await movieRepository.CreateAsync(movie, token);

		var response = MovieMapper.MapToResponse(movie);
		return Results.CreatedAtRoute($"Get{Tag}", new { id = response.Id }, response);
	}

	private static async Task<IResult> Get(
		[FromRoute] int id,
		IMovieRepository movieRepository,
		CancellationToken token)
	{
		var movie = await movieRepository.GetByIdAsync(id, token);

		if (movie is null)
			return Results.NotFound();

		var response = MovieMapper.MapToResponse(movie);
		return Results.Ok(response);
	}

	private static async Task<IResult> GetAll(
		IMovieRepository movieRepository,
		CancellationToken token)
	{
		var movies = await movieRepository.GetAllAsync(token);
		var response = MovieMapper.MapToListResponse(movies);
		return Results.Ok(response);
	}

	private static async Task<IResult> Update(
		[FromRoute] int id,
		[FromBody] MovieRequest request,
		IMovieRepository movieRepository,
		CancellationToken token)
	{
		var existingMovie = await movieRepository.GetByIdAsync(id, token);

		if (existingMovie is null)
			return Results.NotFound();

		var movie = MovieMapper.MapToMovieWithId(request, id);

		movie = await movieRepository.UpdateAsync(movie, token);

		var response = MovieMapper.MapToResponse(movie);

		return Results.Ok(response);
	}

	private static async Task<IResult> Delete(
		[FromRoute] int id,
		IMovieRepository movieRepository,
		CancellationToken token)
	{
		var existingMovie = await movieRepository.GetByIdAsync(id, token);

		if (existingMovie is null)
			return Results.NotFound();

		await movieRepository.DeleteByIdAsync(existingMovie, token);

		return Results.NoContent();
	}
	
}