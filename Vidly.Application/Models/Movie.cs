namespace Vidly.Application.Models;

public class Movie
{
	public int Id { get; set; }

	public string Title { get; init; } = null!;

	public int GenreId { get; init; }
	public Genre Genre { get; set; } = null!;
	public int NumberInStock { get; set; }

	public int DailyRentalRate { get; init; }
	
}