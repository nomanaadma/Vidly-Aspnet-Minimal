using Microsoft.AspNetCore.Mvc;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;
using Vidly.Api.Mappers;
using Vidly.Application.Repositories;

namespace Vidly.Api.Controllers;

[ApiController]
public class MovieController(IMovieRepository movieRepository) : ControllerBase
{
	[HttpPost(ApiEndpoints.Movie.Create)]
	[ProducesResponseType(typeof(MovieResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Create([FromBody]MovieRequest request,
		CancellationToken token)
	{
		var movie = MovieMapper.MapToMovie(request);
		
		movie = await movieRepository.CreateAsync(movie, token);
		
		var response = MovieMapper.MapToResponse(movie);
	
		return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
		
	}
	
	[HttpGet(ApiEndpoints.Movie.Get)]
	[ProducesResponseType(typeof(MovieResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Get([FromRoute] int id,
		CancellationToken token)
	{
		var movie = await movieRepository.GetByIdAsync(id, token);
		
		if (movie is null)
			return NotFound();

		var response = MovieMapper.MapToResponse(movie);
		return Ok(response);
	}
	
	[HttpGet(ApiEndpoints.Movie.GetAll)]
	[ProducesResponseType(typeof(MoviesResponse), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetAll(CancellationToken token)
	{
		var movies = await movieRepository.GetAllAsync(token);
		var response = MovieMapper.MapToListResponse(movies);
		return Ok(response);
	}
	
	
	[HttpPut(ApiEndpoints.Movie.Update)]
	[ProducesResponseType(typeof(MovieResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Update([FromRoute] int id, 
		[FromBody] MovieRequest request, 
		CancellationToken token)
	{
		var existingMovie = await movieRepository.GetByIdAsync(id, token);
		
		if (existingMovie is null)
			return NotFound();
		
		var movie = MovieMapper.MapToMovieWithId(request, id);
		
		movie = await movieRepository.UpdateAsync(movie, token);
		
		var response = MovieMapper.MapToResponse(movie);
		
		return Ok(response);
		
	}
	
	
	[HttpDelete(ApiEndpoints.Movie.Delete)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken token)
	{
		var existingMovie = await movieRepository.GetByIdAsync(id, token);
		
		if (existingMovie is null)
			return NotFound();
		
		await movieRepository.DeleteByIdAsync(existingMovie, token);
		
		return Ok();
	}
	
}
