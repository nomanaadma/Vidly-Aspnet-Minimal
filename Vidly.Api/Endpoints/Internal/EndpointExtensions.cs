using System.Diagnostics;
using System.Reflection;
using Vidly.Api.Helpers;

namespace Vidly.Api.Endpoints;

public static class EndpointExtensions
{
	
	public static void UseEndpoints(this IApplicationBuilder app)
	{
		var typeMarker = typeof(Program);
		var endpointTypes = GetEndpointTypesFromAssemblyContaining(typeMarker);

		foreach (var endpointType in endpointTypes)
		{
			endpointType.GetMethod(nameof(IEndpoints.DefineEndpoints))!
				.Invoke(null, [app]);
		}
	}
	
	public static IEndpointRouteBuilder BaseGroup(this IEndpointRouteBuilder app)
	{
		var baseGroup = app.MapGroup("/api");
		return baseGroup;
	}
	
	public static TBuilder WithMethodName<TBuilder>(
		this TBuilder builder,
		string tag) where TBuilder : IEndpointConventionBuilder
	{
		
		builder.Finally(endpoint =>
		{
			var metadata = endpoint.Metadata;
		
			if (metadata.Count <= 0 || metadata[0] is not MethodInfo methodInfo) return;
		
			var methodName = methodInfo.Name;
			var endpointName = methodName + tag;
			
			endpoint.Metadata.Add(new EndpointNameMetadata(endpointName));
			endpoint.Metadata.Add(new RouteNameMetadata(endpointName));
			endpoint.Metadata.Add(new TagsAttribute(tag));
		
		});
		
		return builder;
	}
	
	private static IEnumerable<TypeInfo> GetEndpointTypesFromAssemblyContaining(Type typeMarker)
	{
		var endpointTypes = typeMarker.Assembly.DefinedTypes
			.Where(x => x is { IsAbstract: false, IsInterface: false } &&
			            typeof(IEndpoints).IsAssignableFrom(x));
		return endpointTypes;
	}
	
}