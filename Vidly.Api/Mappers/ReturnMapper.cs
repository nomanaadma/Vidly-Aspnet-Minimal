using Riok.Mapperly.Abstractions;
using Vidly.Application.Models;
using Vidly.Contracts.Requests;
using Vidly.Contracts.Responses;

namespace Vidly.Api.Mappers;

[Mapper]
public static partial class ReturnMapper
{
	public static partial Return MapToReturn(ReturnRequest rentalRequest);
}