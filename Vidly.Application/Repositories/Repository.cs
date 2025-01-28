using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Vidly.Application.Data;
using Vidly.Application.Models;

namespace Vidly.Application.Repositories;

public class Repository<T>(DatabaseContext context, IValidator<T> validator) 
	: IRepository<T> where T : class
{
	public virtual async Task<T> CreateAsync(T entity, CancellationToken token = default)
	{
		await validator.ValidateAndThrowAsync(entity, token);
		await context.Set<T>().AddAsync(entity, token);
		await context.SaveChangesAsync(token);
		return entity;
	}

	public virtual async Task<T?> GetByIdAsync(int id, CancellationToken token = default)
	{
		var entity = await context.Set<T>().FindAsync([id], token);
		return entity;
	}

	public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default)
	{
		var entities = await context.Set<T>().ToListAsync(token);
		return entities;
	}

	public virtual async Task<T> UpdateAsync(T entity, CancellationToken token = default)
	{
		await validator.ValidateAndThrowAsync(entity, token);
		context.Set<T>().Update(entity);
		await context.SaveChangesAsync(token);
		return entity;
	}

	public async Task DeleteByIdAsync(T entity, CancellationToken token = default)
	{
		context.Set<T>().Remove(entity);
		await context.SaveChangesAsync(token);
	}
}


public interface IGenreRepository : IRepository<Genre>;
public class GenreRepository(DatabaseContext context, IValidator<Genre> validator) 
	: Repository<Genre>(context, validator), IGenreRepository;
	

public interface ICustomerRepository : IRepository<Customer>;
public class CustomerRepository(DatabaseContext context, IValidator<Customer> validator) 
	: Repository<Customer>(context, validator), ICustomerRepository;

