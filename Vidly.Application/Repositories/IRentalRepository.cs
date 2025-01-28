using Vidly.Application.Models;
using Vidly.Contracts.Requests;

namespace Vidly.Application.Repositories;

public interface IRentalRepository
{
	Task<Rental> CreateAsync(Rental rental, CancellationToken token = default);
	
	Task<Rental?> GetByIdAsync(int id, CancellationToken token = default);
	
	Task<IEnumerable<Rental>> GetAllAsync(CancellationToken token = default);
	
	Task<Rental?> FindByReturnAsync(Return rentalReturn, CancellationToken token = default);
	
}