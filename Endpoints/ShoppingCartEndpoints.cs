using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project.Models;
using Project.Services;
using Project.Services.Repository;
using System.Security.Claims;

namespace Project.Endpoints
{
    public static class ShoppingCartEndpoints
    {
        public static void RegisterShoppingEndpoints(this WebApplication app)
        {
            app.MapGet("/ShoppingCart/", async (IRepository<ShoppingCart> cartRepo, HttpContext context) => {

                var userClaims = context.User.Identity as ClaimsIdentity;
                var userId = int.Parse(userClaims.FindFirst("id").Value);


            });

            app.MapPost("/ShoppingCart/AddProduct/{ProductId}/{Quantity}", async (IRepository<ShoppingCart> cartRepo, IRepository<Product> productRepo, int productId, int quantity, HttpContext context) =>
            {
                var userClaims = context.User.Identity as ClaimsIdentity;
                var userId = int.Parse(userClaims.FindFirst("id").Value);

                if (productId < 0) return Results.BadRequest("Product ID need to be a positive integer");

                var product = await productRepo.GetByIdAsync(productId);
                if (product == null) return Results.BadRequest($"Product Id {productId} does not exist.");

                var cartProduct = new ShoppingCart();
                cartProduct.UserId = userId;
                cartProduct.ProductId = product.ProductId;
                cartProduct.Price = product.Price;
                cartProduct.Quantity = quantity;

                await cartRepo.AddAsync(cartProduct);
                return Results.Ok();

            });
        }
    }
}
