namespace Vidly.Application.Models;

public class Rental
{
	public int Id { get; set; }
	
	public int CustomerId { get; init; }
	public Customer Customer { get; set; } = null!;

	public int MovieId { get; init; }
	public Movie Movie { get; set; } = null!;

	public DateTime DateOut { get; init; }
	
	public DateTime? DateReturned { get; set; }
	
	public int RentalFee { get; set; }

}