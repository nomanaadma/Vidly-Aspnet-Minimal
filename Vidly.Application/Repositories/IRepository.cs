namespace Vidly.Application.Repositories;

public interface IRepository<T> where T : class
{
	Task<T> CreateAsync(T entity, CancellationToken token = default);
	
	Task<T?> GetByIdAsync(int id, CancellationToken token = default);
	
	Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default);
	
	Task<T> UpdateAsync(T entity, CancellationToken token = default);
	
	Task DeleteByIdAsync(T entity, CancellationToken token = default);
}