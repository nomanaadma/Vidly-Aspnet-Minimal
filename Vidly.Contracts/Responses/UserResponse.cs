namespace Vidly.Contracts.Responses;

public class UserResponse
{
	public required int Id { get; init; }
	
	public required string Name { get; init; }
	
	public required string Email { get; init; }
	
}