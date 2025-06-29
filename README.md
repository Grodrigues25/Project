# Project
This project has been developed as part of a Software Engineering Dev Academy. 

## Description

This project is a .NET 9 web API designed for software engineers who want to analyze its architecture and features. The codebase demonstrates modern .NET practices and leverages several advanced techniques and libraries to deliver a robust, maintainable API.

## Learning Objectives

- This tool is at this point a work in progress, but it is intended to help me:
  - Understand the architecture of a modern .NET web API.
  - Learn about best practices in .NET development.
  - Explore advanced features like dependency injection, asynchronous programming, and authentication.
- Practice build a RESTful API using .NET 9 and the best practices in software development.
- At a later stage, I will add more features to this project, such as:
  - Adding unit and integration tests to ensure code quality.
  - Implementing advanced error handling and logging mechanisms.
  - Implementing caching strategies to improve performance.
  - Implementing the ability to extract metrics and performance data regarding the "sales" processed by the API of this Order Management Platform.

## What I Learned

- Learned from scratch how to build a RESTful API using .NET 9. This is my first project using .NET 9 and C#.
- Applied for the first time concepts like dependency injection, asynchronous programming, and JWT authentication in a .NET context.
- 
- Concepts that became clearer
- Throughout my development of this project, I've had to go back and forth many time to improve its structure and the way it was being build. You'll be able to see through the commits:
    - The first decision was between using Minimal APIs or using Controllers. I've decided to use Minimal APIs for the simplicity.
    - The change from synchronous programming to asynchronous to improve performance and scalability.
    - The change from manual implementation of the logic of each endpoint to the use of the Repository patter to keep the code DRY.
    - 

## Interesting Techniques

- **Dependency Injection**: The project uses [dependency injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection) to manage service lifetimes and dependencies.
- **Design Patterns**: Implements the [Repository Pattern](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection#repository-pattern) for data access, promoting separation of concerns.
- **Asynchronous Programming**: Implements [async/await](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/) for non-blocking I/O operations.
- **Authentication and Authorization**: Uses [JWT Bearer Authentication](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/jwt-bearer) for secure API access.
- **Model Validation**: Utilizes [data annotations](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations) for input validation.
- **Configuration Management**: Uses [IConfiguration](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration) for flexible app settings.
- **Logging**: Integrates [Microsoft.Extensions.Logging](https://learn.microsoft.com/en-us/dotnet/core/extensions/logging) for structured logging.

## Notable Libraries and Technologies

- [.NET 9](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9)
- [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/) for building web APIs.
- [Swashbuckle.AspNetCore (Swagger)](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) for API documentation.
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/) for data access.
- [ASP.NCore Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity) for user management, password hashing, and JWT token operations.

## Project Structure

- **/Endpoints**: Contains API endpoint logic.
- **/Migrations**: Database migrations for Entity Framework Core.
- **/Models**: Defines data structures and validation.
- **/Services**: Business logic and data access. Includes repository pattern implementations.
- **/Properties**: Project metadata and settings.
---

For more details, see the individual files and directories linked above.