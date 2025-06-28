using Project.Models;
using Project.Services.Repository;
using Project.Services;


namespace Project.Endpoints
{
    public static class ProductEndpoints
    {
        public static void RegisterProductEndpoints(this WebApplication app)
        {
            app.MapGet("/Product", async (IRepository<Product> productRepo) =>
            {
                var productList = await productRepo.GetAsync();
                return Results.Ok(productList);
            });

            
            app.MapGet("/Product/{ProductId}", async (IRepository<Product> productRepo, int productId) =>
            {
                if (productId < 0) return Results.BadRequest("Product ID need to be a positive integer");

                var product = await productRepo.GetByIdAsync(productId);
                return product != null ? Results.Ok(product) : Results.NotFound($"There is no product with ID {productId}.");                
            });


            app.MapPost("/Product", async (IRepository<Product> productRepo, Product newProduct, IAuthenticationService auth, HttpRequest request) =>
            {
                bool tokenIsValid = await auth.ValidateJwtToken(request);
                if (!tokenIsValid) return Results.Unauthorized();

                await productRepo.AddAsync(newProduct);

                return Results.Created($"/Product/{newProduct.ProductId}", newProduct);
            }).RequireAuthorization("adminAccess");


            app.MapPut("/Product/{ProductId}", async (IRepository<Product> productRepo, Product updatedProduct, int productId, IAuthenticationService auth, HttpRequest request) =>
            {
                bool tokenIsValid = await auth.ValidateJwtToken(request);
                if (!tokenIsValid) return Results.Unauthorized();

                if (productId < 0) return Results.BadRequest("Product ID need to be a positive integer");
                if (updatedProduct.ProductId != productId) return Results.BadRequest("Product ID in the body does not match the Product ID in the URL.");

                await productRepo.UpdateAsync(updatedProduct);
                
                return Results.NoContent();
            }).RequireAuthorization("adminAccess");


            app.MapDelete("/Product/{ProductId}", async (IRepository<Product> productRepo, int productId, IAuthenticationService auth, HttpRequest request) =>
            {
                bool tokenIsValid = await auth.ValidateJwtToken(request);
                if (!tokenIsValid) return Results.Unauthorized();

                Product product = await productRepo.GetByIdAsync(productId);
                await productRepo.DeleteAsync(product);
                
                return Results.NoContent();
            }).RequireAuthorization("adminAccess");
        }
    }
}
