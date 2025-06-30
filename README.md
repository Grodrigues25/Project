# Project Order Management Platform
This project is being developed as part of a Software Engineering Dev Academy. 

## Description
This project is a .NET 9 web API build for learning purposes as part of Dev Academy. The codebase demonstrates modern .NET practices and leverages several advanced techniques and libraries to deliver a robust, maintainable API. This project is still a work in progress, and it is intended to serve as a foundation for learning and exploring best practices in .NET development. This is my first project using .NET 9 and C#, so all concepts applied here were learned from scratch throughout 3-4 months and applied as best as possible at this point in time.

## Learning Objectives
- This tool is at this point a work in progress, but it is intended to help me:
  - Understand the architecture of a modern .NET web API.
  - Learn about best practices in .NET development.
  - Explore advanced features like dependency injection, asynchronous programming, authentication, among many others.
  - Practice building a RESTful API using .NET 9 and the best practices in software development.

- At a later stage, I will add more features to this project, such as:
  - Adding unit and integration tests to ensure code quality.
  - Implementing advanced error handling and logging mechanisms.
  - Implementing caching strategies to improve performance.
  - Implementing the ability to extract metrics and performance data regarding the "sales" processed by the API of this Order Management Platform.

## What I Learned
- Learning Points:
  - Learned from scratch how to build a RESTful API using .NET 9. This is my first project using .NET 9 and C#.
  - Applied for the first time concepts like dependency injection, asynchronous programming, and JWT authentication in a .NET context. More below about the main concepts applied.
  - Assisted with working directly with GitHub for version control, including branching, pull requests, handling merge conflicts, and even had to do branch rebases to keep the branches clean and up to date.

- Concepts that became clearer:
    - 

- Throughout my development of this project, I've had to go back and forth many time to improve its structure and the way it was being build. You'll be able to see through the commits the adaptions I made through time:
    - The first decision was between using Minimal APIs or using Controllers. I've decided to use Minimal APIs for the simplicity.
    - The change from synchronous programming to asynchronous to improve performance and scalability.
    - The change from manual implementation of the logic of each endpoint to the use of the Repository patter to keep the code DRY.
    - 

## Interesting Concepts Applied
- **Dependency Injection**: The project uses [dependency injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection) to manage service lifetimes and dependencies.
- **Design Patterns**: Implements the [Repository Pattern](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection#repository-pattern) for data access, promoting separation of concerns.
- **Asynchronous Programming**: Implements [async/await](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/) for non-blocking I/O operations.
- **Authentication and Authorization**: Uses [JWT Bearer Authentication](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/jwt-bearer) for secure API access.
- **Model Validation**: Utilizes [data annotations](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations) for input validation.
- **SQL Full-Text Search**: Implements [full-text search](https://learn.microsoft.com/en-us/ef/core/querying/full-text-search) capabilities in the database for efficient querying.
- **Configuration Management**: Uses [IConfiguration](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration) for flexible app settings.

## Notable Libraries and Technologies
- [.NET 9](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9)
- [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/) for building web APIs.
- [Swashbuckle.AspNetCore (Swagger)](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) for API documentation.
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/) for data access.
- [ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity) for user management, password hashing, and JWT token operations.

## Project Structure
- **/Endpoints**: Contains API endpoint logic:
    - User Endpoints:
        - GET Endpoint for retrieving all users.
        - GET Endpoint to retrieve a user by ID.
        - POST Endpoint for user registration.
        - PUT Endpoint for updating user information.
        - DELETE Endpoint for deleting a user.
    - Product Endpoints:
        - GET Endpoint for retrieving all products.
        - GET Endpoint to retrieve a product by ID.
        - POST Endpoint for adding a new product.
        - PUT Endpoint for updating product information.
        - DELETE Endpoint for deleting a product.
        - GET Endpoint for searching products in Name, Category or Description using SQL Full Text search.
    - Order Endpoints:
        - GET Endpoint for retrieving all orders.
        - GET Endpoint to retrieve an order by ID.
        - POST Endpoint for creating a new order.
        - PUT Endpoint for updating an order.
        - DELETE Endpoint for deleting an order.
    - Shopping Cart Endpoints (IN PROGRESS):
        - GET Endpoint for retrieving the shopping cart of a user.
        - POST Endpoint for adding a product to the shopping cart.
        - PUT Endpoint for updating the quantity of a product in the shopping cart.
        - DELETE Endpoint for removing a product from the shopping cart.
    - Authentication Endpoints:
        - POST Endpoint for user login, returning a JWT token.
        - POST Endpoint for user logout, invalidating the JWT token and placing it in a blacklist table.
- **/Migrations**: Database migrations for Entity Framework Core.
- **/Models**: Defines data structures and validation. Data Annotations are used for model validation.
- **/Services**: Business logic and data access. Includes repository pattern implementations.
- **/Properties**: Project metadata and settings.

## SQL Database Tables
- **Users**: Stores user information, including hashed passwords.
- **Blacklist**: Contains blacklisted tokens for security.
- **Products**: Contains product details.
- **Orders**: Represents customer orders.
- **ShoppingCart**: Manages user shopping carts.
- **OrderItems**: Links products to orders with quantities and prices.

---

For more details, see the individual files and directories linked above.