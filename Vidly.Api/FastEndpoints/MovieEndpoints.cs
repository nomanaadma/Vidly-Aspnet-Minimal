using FastEndpoints;
using Vidly.Api.Mappers;
using Vidly.Application.Repositories;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;

namespace Vidly.Api.FastEndpoints;

public abstract class MovieEndpoints
{
	private static string BaseRoute => "movies";
	
	public class CreateMovie(IMovieRepository movieRepository) 
		: Endpoint<MovieRequest, MovieResponse>
	{
		public override void Configure()
		{
			Post($"{BaseRoute}");
			AllowAnonymous();

			Description(b => b
				.ProducesValidationProblem()
			);
		}
	
		public override async Task HandleAsync(
			MovieRequest request,
			CancellationToken token)
		{
			
			var movie = MovieMapper.MapToMovie(request);
	
			movie = await movieRepository.CreateAsync(movie, token);
	
			var response = MovieMapper.MapToResponse(movie);
			
			await SendCreatedAtAsync<GetMovie>(
				new { id = response.Id },
				response,
				generateAbsoluteUrl: true,
				cancellation: token);
			
		}
		
	}

	
	public class GetMovie(IMovieRepository movieRepository) 
		: EndpointWithoutRequest<MovieResponse>
	{
		public override void Configure()
		{
			Get($"{BaseRoute}/{{id:int}}");
			AllowAnonymous();
			
			Description(b => b
				.Produces(404)
			);
		}
	
		public override async Task HandleAsync(CancellationToken token)
		{

			var id = Route<int>("id");
			
			var movie = await movieRepository.GetByIdAsync(id, token);
	
			if (movie is null)
			{
				await SendNotFoundAsync(token);
				return;
			}
			
			var response = MovieMapper.MapToResponse(movie);
			
			await SendOkAsync(response, token);
	
		}
	}
	
	
	public class GetAllMovie(IMovieRepository movieRepository) 
		: EndpointWithoutRequest<MoviesResponse>
	{
		public override void Configure()
		{
			Get($"{BaseRoute}");
			AllowAnonymous();
			
		}
	
		public override async Task HandleAsync(CancellationToken token)
		{
			
			var movies = await movieRepository.GetAllAsync(token);
			var response = MovieMapper.MapToListResponse(movies);
			
			await SendOkAsync(response, token);
	
		}
	}
	
	
	public class UpdateMovie(IMovieRepository movieRepository) 
		: Endpoint<MovieRequest, MovieResponse>
	{
		public override void Configure()
		{
			Put($"{BaseRoute}/{{id:int}}");
			AllowAnonymous();
			
			Description(b => b
				.Produces(404)
				.ProducesValidationProblem()
			);
		}
	
		public override async Task HandleAsync(
			MovieRequest request,
			CancellationToken token)
		{
			var id = Route<int>("id");
			
			var existingMovie = await movieRepository.GetByIdAsync(id, token);

			if (existingMovie is null)
			{
				await SendNotFoundAsync(token);
				return;
			}

			var movie = MovieMapper.MapToMovieWithId(request, id);

			movie = await movieRepository.UpdateAsync(movie, token);

			var response = MovieMapper.MapToResponse(movie);

			await SendOkAsync(response, token);
			
		}
		
	}
	
	
	public class DeleteMovie(IMovieRepository movieRepository) 
		: EndpointWithoutRequest
	{
		public override void Configure()
		{
			Delete($"{BaseRoute}/{{id:int}}");
			AllowAnonymous();
			
			Description(b => b
				.Produces(404)
				.ProducesValidationProblem()
			);
		}
	
		public override async Task HandleAsync(
			CancellationToken token)
		{
			
			var id = Route<int>("id");
			var existingMovie = await movieRepository.GetByIdAsync(id, token);

			if (existingMovie is null)
			{
				await SendNotFoundAsync(token);
				return;
			}

			await movieRepository.DeleteByIdAsync(existingMovie, token);

			await SendNoContentAsync(token);

		}
		
	}
	
}