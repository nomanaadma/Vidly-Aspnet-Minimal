namespace Vidly.Api;

public static class ApiEndpoints
{
	private const string ApiBase = "/api";
	public static class Genre
	{
		private const string Base = $"{ApiBase}/genres";
		
		public const string Create = Base;
		
		public const string GetAll = Base;
		
		private const string BaseWithId = $"{Base}/{{id}}";
		
		public const string Get = BaseWithId;
		
		public const string Update = BaseWithId;
		
		public const string Delete = BaseWithId;

	}
	
	public static class Customer
	{
		private const string Base = $"{ApiBase}/customers";
		
		public const string Create = Base;
		
		public const string GetAll = Base;
		
		private const string BaseWithId = $"{Base}/{{id}}";
		
		public const string Get = BaseWithId;
		
		public const string Update = BaseWithId;
		
		public const string Delete = BaseWithId;

	}
	
	public static class Movie
	{
		private const string Base = $"{ApiBase}/movies";
		
		public const string Create = Base;
		
		public const string GetAll = Base;
		
		private const string BaseWithId = $"{Base}/{{id}}";
		
		public const string Get = BaseWithId;
		
		public const string Update = BaseWithId;
		
		public const string Delete = BaseWithId;

	}
	
	public static class Rental
	{
		private const string Base = $"{ApiBase}/rentals";
		
		public const string Create = Base;
		
		public const string GetAll = Base;
		
		private const string BaseWithId = $"{Base}/{{id}}";
		
		public const string Get = BaseWithId;
		
		public const string Update = BaseWithId;
		
		public const string Delete = BaseWithId;

	}
	
	public static class Return
	{
		private const string Base = $"{ApiBase}/returns";
		
		public const string Create = Base;

	}
	
	public static class User
	{
		private const string Base = $"{ApiBase}/users";
		
		public const string Create = Base;
		
		public const string Me = $"{ApiBase}/me";
		
	}
	
	public const string Auth = $"{ApiBase}/auth";
	
}