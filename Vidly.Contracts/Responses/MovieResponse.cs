namespace Vidly.Contracts.Responses;

public class MoviesResponse
{
	public required IEnumerable<MovieResponse> Items { get; init; }
}

public class MovieResponse
{
	public required int Id { get; init; }
	
	public required string Title { get; init; }
	
	public required GenreResponse Genre { get; init; }
	
	public required int NumberInStock { get; init; }

	public required int DailyRentalRate { get; init; }
	
}