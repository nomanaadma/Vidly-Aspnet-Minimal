namespace Vidly.Application.Models;

public class User
{
	public int Id { get; init; }
	
	public string Name { get; init; } = null!;
	
	public string Email { get; init; } = null!;
	
	public string Password { get; set; } = null!;

	public bool IsAdmin { get; init; }
	
}