using System.ComponentModel.DataAnnotations;

namespace Vidly.Contracts.Requests;

public class AuthRequest
{
	[Required]
	public string? Email { get; init; }
	
	[Required]
	public string? Password { get; init; }
}