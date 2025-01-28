using Vidly.Application.Models;

namespace Vidly.Application.Repositories;

public interface IUserRepository
{
	Task<User> CreateAsync(User user, CancellationToken token = default);
	
	Task<User?> GetByEmailAsync(string email, CancellationToken token = default);

	Task<User?> GetByIdAsync(int id, CancellationToken token = default);
}