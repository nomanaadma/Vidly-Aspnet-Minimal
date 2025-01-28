namespace Vidly.Contracts.Responses;

public class GenresResponse
{
	public required IEnumerable<GenreResponse> Items { get; init; }
}

public class GenreResponse
{
	public required int Id { get; init; }
	
	public required string Name { get; init; }
}