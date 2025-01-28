namespace Vidly.Application.Models;

public class Customer
{
	public int Id { get; set; }

	public string Name { get; init; } = null!;
	
	public bool IsGold { get; init; }
	
	public string Phone { get; init; } = null!;
}