using Microsoft.EntityFrameworkCore;
using Vidly.Application.Data;
using Vidly.Application.Models;

namespace Vidly.Application.Repositories;

public class UserRepository(DatabaseContext context) : IUserRepository
{
	public async Task<User> CreateAsync(User user, CancellationToken token = default)
	{
		await context.Users.AddAsync(user, token);
		await context.SaveChangesAsync(token);
		return user;
	}

	public async Task<User?> GetByEmailAsync(string email, CancellationToken token = default)
	{
		var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email, token);
		return user;
	}
	
	public async Task<User?> GetByIdAsync(int id, CancellationToken token = default)
	{
		var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id, token);
		return user;
	}
}