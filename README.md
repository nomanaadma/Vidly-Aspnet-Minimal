# Vidly API

Rewrite of [Vidly Aspnet](https://github.com/nomanaadma/Vidly-Aspnet) in **Asp.Net Core** with **Minimal API** and **FastEndPoint**.

Vidly is a RESTful API service for managing a movie rental system, built with ASP.NET Core. It provides endpoints for managing movies, customers, rentals, and user authentication.


## Purpose of This Repository

This repository serves as a personal record of my learning journey. It helps me track my progress, understand concepts better by implementing them, and refer back to the code snippets and examples in the future.


## Technical Stack

- **Framework**: ASP.NET Core
- **Architecture**: RESTful API using Minimal api and FastEndpoints.
- **Database**: PostgreSQL with Entity Framework Core
- **Authentication**: JWT (JSON Web Tokens)
- **Validation**: FluentValidation for request model validation
- **Documentation**: Swagger/OpenAPI
- **ORM**: Entity Framework Core with Code-First approach

## Features

- User authentication and authorization using JWT tokens
- Movie management with genres
- Customer management
- Rental processing
- Returns handling
- User management
- Request validation using FluentValidation
- Entity Framework Core with PostgreSQL
- Role-based authorization

## API Endpoints

### Authentication
- `POST /api/auth` - Authenticate user
- `POST /api/users` - Register new user
- `GET /api/me` - Get current user profile

### Movies
- `GET /api/movies` - List all movies
- `GET /api/movies/{id}` - Get movie details
- `POST /api/movies` - Add new movie
- `PUT /api/movies/{id}` - Update movie
- `DELETE /api/movies/{id}` - Delete movie

### Genres
- `GET /api/genres` - List all genres
- `GET /api/genres/{id}` - Get genre details
- `POST /api/genres` - Add new genre
- `PUT /api/genres/{id}` - Update genre
- `DELETE /api/genres/{id}` - Delete genre

### Customers
- `GET /api/customers` - List all customers
- `GET /api/customers/{id}` - Get customer details
- `POST /api/customers` - Add new customer
- `PUT /api/customers/{id}` - Update customer
- `DELETE /api/customers/{id}` - Delete customer

### Rentals
- `GET /api/rentals` - List all rentals
- `GET /api/rentals/{id}` - Get rental details
- `POST /api/rentals` - Create new rental
- `POST /api/returns` - Process movie return

## Data Models

### Movie
```json
{
  "title": "string",
  "genreId": "integer",
  "numberInStock": "integer",
  "dailyRentalRate": "integer"
}
```

### Customer
```json
{
  "name": "string",
  "phone": "string",
  "isGold": "boolean"
}
```

### Rental
```json
{
  "customerId": "integer",
  "movieId": "integer"
}
```

### User
```json
{
  "email": "string",
  "password": "string",
  "name": "string",
  "isAdmin": "boolean"
}
```

## Implementation Details

### Entity Framework Core
- Code-First approach with entity configurations
- PostgreSQL database provider
- Migration support for database versioning
- Repository pattern for data access

### Authentication & Authorization
- JWT token-based authentication
- Role-based authorization (Admin and User roles)
- Password hashing and security
- Token validation and refresh mechanisms

### Validation
- FluentValidation for request model validation
- Custom validation rules
- Automatic model state validation
- Validation error responses

### Error Handling
The API uses standard HTTP response codes:
- 200: Success
- 201: Created
- 400: Bad Request
- 404: Not Found

Validation errors return a `ValidationProblemDetails` object with specific error messages.

## Getting Started

1. Clone the repository
2. Install dependencies:
    - .NET Core SDK
    - PostgreSQL
3. Configure your database connection in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=vidly;Username=your_username;Password=your_password"
  }
}
```
4. Run migrations:
```bash
dotnet ef database update
```
5. Start the API server:
```bash
dotnet run
```

## API Documentation

Full API documentation is available via Swagger UI at `/swagger` when running the application in development mode.

## Authentication Flow

1. Register a new user:
```bash
POST /api/users
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "your_password",
  "name": "John Doe"
}
```

2. Login to get JWT token:
```bash
POST /api/auth
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "your_password"
}
```

3. Use the token in subsequent requests:
```bash
GET /api/movies
Authorization: Bearer your_jwt_token
```