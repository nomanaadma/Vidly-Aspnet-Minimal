using FastEndpoints;
using Vidly.Api.Mappers;
using Vidly.Application.Repositories;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;

namespace Vidly.Api.FastEndpoints;

public abstract class GenreEndpoints
{
	private static string BaseRoute => "genres";
	
	public class CreateGenre(IGenreRepository genreRepository) 
		: Endpoint<GenreRequest, GenreResponse>
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
			GenreRequest request,
			CancellationToken token)
		{
			
			var genre = GenreMapper.MapToGenre(request);
	
			genre = await genreRepository.CreateAsync(genre, token);
	
			var response = GenreMapper.MapToResponse(genre);
			
			await SendCreatedAtAsync<GetGenre>(
				new { id = response.Id },
				response,
				generateAbsoluteUrl: true,
				cancellation: token);
			
		}
		
	}

	
	public class GetGenre(IGenreRepository genreRepository) 
		: EndpointWithoutRequest<GenreResponse>
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
			
			var genre = await genreRepository.GetByIdAsync(id, token);
	
			if (genre is null)
			{
				await SendNotFoundAsync(token);
				return;
			}
			
			var response = GenreMapper.MapToResponse(genre);
			
			await SendOkAsync(response, token);
	
		}
	}
	
	
	public class GetAllGenre(IGenreRepository genreRepository) 
		: EndpointWithoutRequest<GenresResponse>
	{
		public override void Configure()
		{
			Get($"{BaseRoute}");
			AllowAnonymous();
			
		}
	
		public override async Task HandleAsync(CancellationToken token)
		{
			
			var genres = await genreRepository.GetAllAsync(token);
			var response = GenreMapper.MapToListResponse(genres);
			
			await SendOkAsync(response, token);
	
		}
	}
	
	
	public class UpdateGenre(IGenreRepository genreRepository) 
		: Endpoint<GenreRequest, GenreResponse>
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
			GenreRequest request,
			CancellationToken token)
		{
			var id = Route<int>("id");
			
			var existingGenre = await genreRepository.GetByIdAsync(id, token);

			if (existingGenre is null)
			{
				await SendNotFoundAsync(token);
				return;
			}

			var genre = GenreMapper.MapToGenreWithId(request, id);

			genre = await genreRepository.UpdateAsync(genre, token);

			var response = GenreMapper.MapToResponse(genre);

			await SendOkAsync(response, token);
			
		}
		
	}
	
	
	public class DeleteGenre(IGenreRepository genreRepository) 
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
			var existingGenre = await genreRepository.GetByIdAsync(id, token);

			if (existingGenre is null)
			{
				await SendNotFoundAsync(token);
				return;
			}

			await genreRepository.DeleteByIdAsync(existingGenre, token);

			await SendNoContentAsync(token);

		}
		
	}
	
}