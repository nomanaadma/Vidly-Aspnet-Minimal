using FluentValidation;
using Vidly.Application.Models;
using Vidly.Application.Repositories;

namespace Vidly.Application.Validators;

public class UserValidator : AbstractValidator<User>
{
	private readonly IUserRepository _userRepository;
	
	public UserValidator(IUserRepository userRepository)
	{
		_userRepository = userRepository;
		
		RuleFor(u => u.Name)
			.Length(5, 50)
			.NotEmpty();
		
		RuleFor(u => u.Password)
			.Length(5, 1024)
			.NotEmpty();
		
		RuleFor(u => u.Email)
			.Length(5, 255)
			.EmailAddress()
			.NotEmpty();
		
		RuleFor(u => u.Email)
			.MustAsync(ValidateEmail)
			.WithMessage("User already registered");
		
	}
	
	private async Task<bool> ValidateEmail(User user, string email, CancellationToken token = default)
	{
		var existingUser = await _userRepository.GetByEmailAsync(email, token);
		return existingUser is null;
	}
}