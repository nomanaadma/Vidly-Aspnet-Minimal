using Riok.Mapperly.Abstractions;
using Vidly.Application.Models;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;

namespace Vidly.Api.Mappers;

[Mapper]
public static partial class GenreMapper
{
	public static partial GenreResponse MapToResponse(Genre genre);
	
	public static GenresResponse MapToListResponse(IEnumerable<Genre> genres) => 
		new() { Items = genres.Select(MapToResponse) };

	public static partial Genre MapToGenre(GenreRequest genreRequest);

	public static Genre MapToGenreWithId(GenreRequest genreRequest, int id)
	{
		var genre = MapToGenre(genreRequest);
		genre.Id = id;
		return genre;
	}

}