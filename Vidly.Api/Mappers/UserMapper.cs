using Riok.Mapperly.Abstractions;
using Vidly.Application.Models;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;

namespace Vidly.Api.Mappers;

[Mapper]
public static partial class UserMapper
{
	public static partial User MapToUser(UserRequest userRequest);
	
	public static partial UserResponse MapToResponse(User user);
}
