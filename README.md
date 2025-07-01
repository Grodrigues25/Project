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

## What I Learned
- Learning Points:
  - Learned from scratch how to build a RESTful API using .NET 9. This is my first project using .NET 9 and C#.
  - Applied for the first time concepts like dependency injection, asynchronous programming, and JWT authentication in a .NET context. More below about the main concepts applied.
  - Assisted with working directly with GitHub for version control, including branching, pull requests, handling merge conflicts, and even had to do branch rebases to keep the branches clean and up to date.
  - Deepened knowledge on SQL querying, including the use of full-text search capabilities in SQL Server, and how to implement it in a .NET context.
  - Learn to use Entity Framework Core for data access, including migrations and model validation, and build a code first approach to database design.

- Throughout my development of this project, I've had to go back and forth many time to improve its structure and the way it was being build. You'll be able to see through the commits the adaptions I made through time:
    - The first decision was between using Minimal APIs or using Controllers. I've decided to use Minimal APIs for the simplicity.
    - The change from synchronous programming to asynchronous to improve performance and scalability.
    - The change from manual implementation of the logic of each endpoint to the use of the Repository patter to keep the code DRY.

## Interesting Concepts Applied 
- **Dependency Injection**: The project uses [dependency injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection) to manage service lifetimes and dependencies.
- **Design Patterns**: Implements the [Repository Pattern](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection#repository-pattern) for data access, promoting separation of concerns.
- **Asynchronous Programming**: Implements [async/await](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/) for non-blocking I/O operations.
- **Authentication and Authorization**: Uses [JWT Bearer Authentication](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/jwt-bearer) for secure API access. Implements blacklisting of JWT tokens to prevent reuse after logout.
- **Model Validation**: Utilizes [data annotations](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations) for input validation.
- **SQL Full-Text Search**: Implements [full-text search](https://learn.microsoft.com/en-us/ef/core/querying/full-text-search) capabilities in the database for efficient querying.
- **Configuration Management**: Uses [IConfiguration](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration) for flexible app settings.

## Notable Libraries and Technologies
- [.NET 9](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9)
- [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/) for building web APIs.
- [Minimal APIs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis) for simplified API development.
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/) for data access.
- [ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity) for user management, password hashing, and JWT token operations.
- [Azure SQL Database](https://learn.microsoft.com/en-us/azure/azure-sql/database/) for relational database.

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
    - Shopping Cart Endpoints:
        - GET Endpoint for retrieving the shopping cart of a user.
        - GET Endpoint for retrieving the items of the shopping cart of the user.
        - POST Endpoint for adding a product to the shopping cart.
        - PUT Endpoint for updating the quantity of a product in the shopping cart.
        - DELETE Endpoint for removing a product from the shopping cart.
        - DELETE Endpoint for clearing the entire shopping cart. 
        - POST Endpoint to submit the cart into an order.
    - Authentication Endpoints:
        - POST Endpoint for user login, returning a JWT token.
        - POST Endpoint for user logout, invalidating the JWT token and placing it in a blacklist table.
    - Sales Reports:
        - GET Endpoint to retrieve sales reports for a given month;
        - GET Endpoint to retrieve sales reports for a given year;
        - GET Endpoint to retrieve the total sales since the start;
        - GET Endpoint to retrieve the most sold product since the start;
        - GET Endpoint to retrieve the top customers list since the start.

- **/Migrations**: Database migrations for Entity Framework Core.
- **/Models**: Defines data structures and validation. Data Annotations are used for model validation. In general, it implements the schema of the SQL Tables in the database and some support models for responses in certain endpoints. Models for SQL DB tables are represented below in description of SQL DB tables. Placing below description of the other support models for the APIs:
    - **Authentication**:
        - LoginRequestModel: Implements what are the mandatory parameters required in the LoginRequest.
        - LoginResponseModel: Implements what are the mandatory parameters sent through the endpoint in the event of a successful LoginRequest.
    - **Reports**:
        - ReportTopCustomersModel: Implements the response sent through the endpoint to the GET request to get Top Customers list;
        - ReportTopProdcutsModel: Implements the response sent through the endpoint to the GET request to get the Best Selling Products list;
    - **ShoppingCart**:
        - AddToCartModelResponseModel: Implements the response sent back to user when using POST request to add an item to the cart;

 - **/Services**: Business logic and data access. Whenever applicable, repository pattern is used to abstract the data access logic from the endpoints:
    - **AuthenticationService**: Handles user authentication and JWT token management and is used to abstract the authentication logic from the endpoints. Also used to support authorization logic in the endpoints.
    - **Database**: Contains the database context and configuration for Entity Framework Core, along with the table foreign keys and relationships. Contains as well the DatabaseService which is a helper service to abstract the Fullext Search logic from the endpoints.
    - **Reports**: Contains the service logic for querying the SQLDatabase tables to generate the sales data for the report endpoints.
    - **ShoppingCartService**: Contains the service logic for managing the shopping cart, including adding, updating, and removing items.
    - **Repository**: Used to implement generic AddAsync, DeleteAsync, GetAsync, GetByIdAsync, UpdateAsync using Repository pattern for all kinds of operations for the tables involved in this project with Entity Framework. It is implemented as a template to be adaptable to the class provided.
    - **SecurityService**: Helper service to handle security-related operations, such as password hashing.
- **/Properties**: Project metadata and settings.

### Note on API Endpoint Testing in Postman/Bruno
In the project files you can find the files DevAcademy.json and DevAcademyPostman.json. The first file contains the endpoint testing as exported from Bruno. The second is the Postman version. Throughout the development I've tested the endpoints using Bruno. 

# API Endpoint Responses
The Microsoft.AspNetCore.Http.Results namespace is used to define the responses for the API endpoints. The following main ones were relied upon on this project:
- **Results.Ok**: Used to return a 200 OK response with the requested data.
- **Results.Created**: Used to return a 201 Created response when a new resource is successfully created.
- **Results.NoContent**: Used to return a 204 No Content response when an operation is successful but there is no content to return (e.g., after a DELETE operation).
- **Results.BadRequest**: Used to return a 400 Bad Request response when the request is invalid or missing required parameters.
- **Results.Unauthorized**: Used to return a 401 Unauthorized response when the user is not authenticated or the JWT token is invalid.
- **Results.NotFound**: Used to return a 404 Not Found response when a requested resource does not exist.
- **Results.InternalServerError**: Used to return a 500 Internal Server Error response when an unexpected error occurs.

### Note on Endpoint Responses
- The PUT endpoints all enforce full object details to be provided in request body to make sure all PUT requests are idempotent and the object is fully replaced in the database.

# JWT Authentication
The project implements JWT (JSON Web Token) authentication for secure API access. The authentication flow is as follows:
1. **User Registration**: Users can register by providing their email and password. Passwords are hashed using the `PasswordHasher` class.
2. **User Login**: Users can log in with their email and password. Upon successful authentication, a JWT token is generated and returned to the user. The logic for this process is handled by the AuthenticationService, specifically the Authenticate method. The JWT token was provided an expiration time of 30 minutes by default.
3. **Token Blacklisting**: When a user logs out, the JWT token is added to a blacklist table to prevent reuse. The `BlacklistService` checks incoming tokens against this blacklist.

# Authorization
- In situations where only the Admin users should have access to the endpoint, the minimal API RequiresAuthorization method is used to enforce authorization. The "adminAccess" policy created in Program.cs is used to enforce the authorization.
- In situations where only Users or Admins should have access to the endpoint, I check the JWT token for the user role and enforce authorization accordingly. The AuthenticationService is used to check if the user has the User or Admin role.
- In situations where any kind of user should have access to the endpoint, no authorization checks are made.

### Note on Authorization
I did not find a good way to make the RequireAuthorization method from Minimal APIs work with multiple policies, with logic where if any single one of the policies were valid for the token, the endpoint would be accessible. Given this limitation, I opted to implement the authorization checks directly in the endpoint logic using the AuthenticationService in endpoint where either User or Admin roles should have access.

# SQL Database
For the Database I use Azure SQL Database. I configured the connection using the connection string in appsettings.json. The authentication used in Active Directory Default.

## SQL Database Tables
- **Users**: Stores user information, including hashed passwords.
- **Blacklist**: Contains blacklisted tokens for security.
- **Products**: Contains product details.
- **Orders**: Represents customer orders.
- **OrderItems**: Links products to orders with quantities and prices.
- **ShoppingCart**: Manages user shopping carts.
- **ShoppingCartItems**: Link user's shopping carts to the items in them.

![SQL DBs schema and relationships](https://github.com/Grodrigues25/Project/blob/main/Images/SQLdbSchemaAndRelationships.png?raw=true)

Entity Framework Implementation of Foreign Key Constraints
```c#
        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // https://learn.microsoft.com/en-us/ef/core/modeling/generated-properties?tabs=data-annotations

            modelBuilder.Entity<User>()
                .HasIndex(user => user.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany<Order>()
                .WithOne()
                .HasForeignKey(order => order.UserId)
                .IsRequired();

            // Order Items Constraints
            modelBuilder.Entity<Order>()
                .HasMany<OrderItems>()
                .WithOne()
                .HasForeignKey(orderItem => orderItem.OrderId)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .HasMany<OrderItems>()
                .WithOne()
                .HasForeignKey(orderItem => orderItem.ProductId)
                .IsRequired();

            // Shopping Cart Constraints
            modelBuilder.Entity<User>()
                .HasMany<ShoppingCart>()
                .WithOne()
                .HasForeignKey(ShoppingCartItem => ShoppingCartItem.UserId)
                .IsRequired();

            // Shopping Cart Items Constraints
            modelBuilder.Entity<ShoppingCart>()
                .HasMany<ShoppingCartItems>()
                .WithOne()
                .HasForeignKey(cartItem => cartItem.CartId)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .HasMany<ShoppingCartItems>()
                .WithOne()
                .HasForeignKey(cartItem => cartItem.ProductId)
                .IsRequired();
        }
        #endregion
```

## SQL Full-Text Search
SQL Full-Text Search was used for simplicity and easy of implementation.

CATALOG CREATION:
```sql
CREATE FULLTEXT CATALOG ProductSearch AS DEFAULT;
```

SQL Full-Text Index Creation:
```sql
CREATE FULLTEXT INDEX ON product(Name, Description, Category)
  KEY INDEX PK_Product ON ProductSearch 
  WITH STOPLIST = OFF, CHANGE_TRACKING AUTO;
```
---

## Additional Notes:
- For C# style enforcement, StyleCop Analyser is used to ensure code quality and consistency.

---
## Improvements and Future Work
- At a later stage, I will add more features to this project, such as:
    - The JWT token key use to sign the tokens should be stored in Azure Key Vault or equivalent for enhanced security.
    - Implementing Refresh Tokens for JWT authentication to enhance security and user experience.
    - Adding unit and integration tests to ensure code quality.
    - Implementing advanced error handling and logging mechanisms.
    - Implementing caching strategies to improve performance.
    - If I were to deploy this API I would:
        - Use Azure App Service for hosting the API.
        - Use Service Principal Authentication for secure access to Azure resources, namely the Azure SQL DB supporting the API.


---
For more details, see the individual files and directories linked above.