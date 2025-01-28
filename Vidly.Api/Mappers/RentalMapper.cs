using Riok.Mapperly.Abstractions;
using Vidly.Application.Models;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;

namespace Vidly.Api.Mappers;

[Mapper]
public static partial class RentalMapper
{
	public static partial RentalResponse MapToResponse(Rental rental);
	
	public static RentalsResponse MapToListResponse(IEnumerable<Rental> rentals) => 
		new() { Items = rentals.Select(MapToResponse) };

	public static partial Rental MapToRental(RentalRequest rentalRequest);

	public static Rental MapToRentalWithId(RentalRequest rentalRequest, int id)
	{
		var rental = MapToRental(rentalRequest);
		rental.Id = id;
		return rental;
	}

}