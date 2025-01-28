using System.ComponentModel.DataAnnotations;

namespace Vidly.Contracts.Requests;

public class UserRequest : AuthRequest
{
	public string? Name { get; init; }
	public bool IsAdmin { get; init; }
}