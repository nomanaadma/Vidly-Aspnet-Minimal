using Riok.Mapperly.Abstractions;
using Vidly.Application.Models;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;

namespace Vidly.Api.Mappers;

[Mapper]
public static partial class MovieMapper
{
	public static partial MovieResponse MapToResponse(Movie movie);
	
	public static MoviesResponse MapToListResponse(IEnumerable<Movie> movies) => 
		new() { Items = movies.Select(MapToResponse) };

	public static partial Movie MapToMovie(MovieRequest movieRequest);

	public static Movie MapToMovieWithId(MovieRequest movieRequest, int id)
	{
		var movie = MapToMovie(movieRequest);
		movie.Id = id;
		return movie;
	}

}