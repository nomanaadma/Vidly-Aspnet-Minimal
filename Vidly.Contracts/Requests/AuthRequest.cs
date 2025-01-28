using System.ComponentModel.DataAnnotations;

namespace Vidly.Contracts.Requests;

public class AuthRequest
{
	public string? Email { get; init; }
	
	public string? Password { get; init; }
}