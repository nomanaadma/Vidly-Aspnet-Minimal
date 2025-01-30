using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Vidly.Api.Endpoints;
using Vidly.Api.Handlers;
using Vidly.Api.Middlewares;
using Vidly.Application;
using Vidly.Application.Data;
using FastEndpoints;
using FastEndpoints.Swagger;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((ctx, cf) =>
{
	cf.WriteTo.Console();
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(config =>
{
	config.ShortSchemaNames = true;
});

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var config = builder.Configuration;

var jwtKey = config["JwtTokenSecret"] ??
    throw new InvalidOperationException("Token string 'JwtTokenSecret' not found.");

var connectionString = config.GetConnectionString("database") ??
                       throw new InvalidOperationException("Connection string 'database' not found.");

builder.Services.AddApplication(config);
builder.Services.AddDatabase(connectionString);

builder.Services.AddAuthentication(o =>
{
	o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(t =>
{
	t.TokenValidationParameters = new TokenValidationParameters
	{
		IssuerSigningKey = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(jwtKey)),
		ValidateIssuerSigningKey = true,
		ValidateLifetime = true,
		ValidateIssuer = false,
		ValidateAudience = false, 
	};
	
	t.Events = new JwtBearerEvents
	{
		OnChallenge = context =>
		{
			context.HandleResponse(); // Prevent the default response handling
	
			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			context.Response.ContentType = "application/json";
	
			var problemDetails = new ProblemDetails
			{
				Status = StatusCodes.Status401Unauthorized,
				Title = "Unauthorized",
				Detail = "You are not authorized to access this resource.",
				Instance = context.HttpContext.Request.Path
			};
	
			var jsonResponse = JsonSerializer.Serialize(problemDetails);
			return context.Response.WriteAsync(jsonResponse);
		},
		OnForbidden = context =>
		{
			context.Response.StatusCode = StatusCodes.Status403Forbidden;
			context.Response.ContentType = "application/json";
		
			var problemDetails = new ProblemDetails
			{
				Status = StatusCodes.Status403Forbidden,
				Title = "Forbidden",
				Detail = "You do not have permission to access this resource.",
				Instance = context.HttpContext.Request.Path
			};
		
			var jsonResponse = JsonSerializer.Serialize(problemDetails);
			return context.Response.WriteAsync(jsonResponse);
		}
	};
	
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Admin", p => 
	    p.RequireClaim("IsAdmin", "true"));

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
// 	app.UseSwagger();
// 	app.UseSwaggerUI();
// }

app.MapHealthChecks("/_health");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.UseMiddleware<ValidationMiddleware>();

app.UseFastEndpoints(c =>
{
	c.Endpoints.RoutePrefix = "api";
	
	// c.Endpoints.Configurator = ep =>
	// {
	// 	ep.AllowAnonymous();
	// };
});

app.UseSwaggerGen();

// app.UseEndpoints();

var scope = app.Services.CreateScope();
var dbInitializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
await dbInitializer.InitializeAsync();

app.Run();


