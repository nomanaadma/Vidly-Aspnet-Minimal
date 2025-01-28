using FluentValidation;
using Vidly.Application.Repositories;
using Vidly.Contracts.Requests;
using BC = BCrypt.Net.BCrypt;

namespace Vidly.Application.Validators;

public class AuthValidator : AbstractValidator<AuthRequest>
{
	private readonly IUserRepository _userRepository;
	
	public AuthValidator(IUserRepository userRepository)
	{
		_userRepository = userRepository;
		
		RuleFor(a => a.Password)
			.Length(5, 1024)
			.NotEmpty();
		
		RuleFor(a => a.Email)
			.Length(5, 255)
			.EmailAddress()
			.NotEmpty();
		
		RuleFor(a => a)
			.MustAsync(ValidateAuth)
			.WithName("User")
			.WithMessage("Invalid email or password");
		
	}

	private async Task<bool> ValidateAuth(AuthRequest auth, CancellationToken token)
	{
		var existingUser = await _userRepository.GetByEmailAsync(auth.Email, token);

		return existingUser is not null && BC.Verify(auth.Password, existingUser.Password);
	}
	
}