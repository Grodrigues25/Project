using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Models.ShoppingCart;
using Project.Services;
using Project.Services.Repository;
using System.Security.Claims;

namespace Project.Endpoints
{
    public static class ShoppingCartEndpoints
    {
        public static void RegisterShoppingCartEndpoints(this WebApplication app)
        {
            app.MapGet("/ShoppingCart/", async (IRepository<ShoppingCart> cartRepo, HttpContext context, UserDbContext dbcontext) => {

                var userClaims = context.User.Identity as ClaimsIdentity;
                var userId = int.Parse(userClaims.FindFirst("id").Value);

                var userCart = await dbcontext.shoppingCarts
                    .Where(cart => cart.UserId == userId && !cart.isCheckedOut)
                    .FirstOrDefaultAsync();

                return Results.Ok(userCart);

            });

            app.MapGet("/ShoppingCart/Items", async (IRepository<ShoppingCartItems> cartItemsRepo, HttpContext context, UserDbContext dbContext) => {
                var userClaims = context.User.Identity as ClaimsIdentity;
                var userId = int.Parse(userClaims.FindFirst("id").Value);

                var userCart = await dbContext.shoppingCarts
                    .Where(cart => cart.UserId == userId && !cart.isCheckedOut)
                    .FirstOrDefaultAsync();

                if (userCart == null)
                {
                    return Results.NotFound("No active shopping cart found for the user.");
                }

                var userCartItems = await dbContext.shoppingCartItems
                .Where(item => item.CartId == userCart.CartId && !userCart.isCheckedOut).ToListAsync();

                return Results.Ok(userCartItems);
            });

            app.MapPost("/ShoppingCart/AddProduct/{ProductId}/{Quantity}", async (IShoppingCartService cartService, IRepository<ShoppingCart> cartRepo, IRepository<Product> productRepo, IRepository<ShoppingCartItems> itemsCartRepo, int productId, int quantity, HttpContext context) =>
            {
                var userClaims = context.User.Identity as ClaimsIdentity;
                try
                {
                    var userId = int.Parse(userClaims.FindFirst("id").Value);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex);
                }

                if (productId < 0) return Results.BadRequest("Product ID need to be a positive integer");

                var product = await productRepo.GetByIdAsync(productId);
                if (product == null) return Results.BadRequest($"Product Id {productId} does not exist.");

                if (product.Stock - quantity < 0) return Results.BadRequest($"Product Id {productId} does not have enough stock. Available stock: {product.Stock}");
                
                var addToCartResponse = await cartService.AddToCart(cartRepo, itemsCartRepo, product, quantity, context);

                if (addToCartResponse == null) return Results.BadRequest("Failed to add product to cart.");
                return Results.Ok(addToCartResponse);

            });
        }
    }
}
