namespace Vidly.Api.Endpoints.Internal;

public interface IEndpoints
{
	static abstract void DefineEndpoints(IEndpointRouteBuilder app);
}