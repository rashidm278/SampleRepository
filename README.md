Search Project

Search Project API Documentation

Overview

Search Project is a .NET Core Web API that allows users to search and filter products based on various criteria. The API uses MediatR for handling requests, a repository pattern for database access, and a logging middleware to log requests and responses.

#Prerequisites

Before running the project, ensure you have the following installed:

1).NET Core SDK (latest version)

2)Visual Studio Code / Visual Studio

3)SQL Server

4)Entity Framework Core Tools

5)Git

Installation & Setup
Clone the Repository

Open in VS Code / Visual Studio
If using VS Code:

code .

If using Visual Studio, open the .sln file.

Configure Database Connection

Run Entity Framework Migrations
Run the following commands to create and apply database migrations:
dotnet restore dotnet ef migrations add InitialCreate --project SearchProject.Repository --startup-project SearchProject.Api dotnet ef database update --project SearchProject.Infrastructure --startup-project SearchProject.Api

Build and Run the Solution
dotnet build dotnet run --project SearchProject.Api

Open Swagger UI
Once the API is running, open the following URL in your browser:

http://localhost:5238/swagger

This will show all available endpoints and allow testing.

Authentication (JWT Token)

The API uses JWT authentication.
The response will contain a JWT token.

Copy the token and include it in the Authorization header for authenticated requests.

Authorization: Bearer

Product Controller

Endpoints

Search for Products by Keyword

Filter Products sort product

Architecture & Design Patterns
MediatR: Handles requests via command and query handlers.

Repository Pattern: Provides abstraction for database access i usedIn memory .

Middleware: Logs all incoming requests and outgoing responses, Custom error handling middleware.

Command Handler & Service Layer: Ensures separation of concerns.
