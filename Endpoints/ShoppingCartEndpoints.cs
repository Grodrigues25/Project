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
            app.MapGet("/ShoppingCart/", async (IShoppingCartService cartService, HttpContext context) => {

                var userCart = await cartService.GetUserCartAsync(context);
                return userCart != null ? Results.Ok(userCart) : Results.NotFound("No active shopping cart found for the user.");

            });

            app.MapGet("/ShoppingCart/Items", async (IShoppingCartService cartService, HttpContext context) => {

                var userCart = await cartService.GetUserCartAsync(context);
                if (userCart == null) return Results.NotFound("No active shopping cart found for the user.");

                var userCartItems = await cartService.GetShoppingCartItemsAsync(context, userCart);

                return userCartItems != null ? Results.Ok(userCartItems) : Results.InternalServerError("Failed to collect user cart items");
            });

            app.MapPost("/ShoppingCart/{ProductId}/{Quantity}", async (IShoppingCartService cartService, IRepository<Product> productRepo, int productId, int quantity, HttpContext context) =>
            {
                if (productId < 0) return Results.BadRequest("Product ID need to be a positive integer");

                var product = await productRepo.GetByIdAsync(productId);
                if (product == null) return Results.BadRequest($"Product Id {productId} does not exist.");

                if (product.Stock - quantity < 0) return Results.BadRequest($"Product Id {productId} does not have enough stock. Available stock: {product.Stock}");
                
                var addToCartResponse = await cartService.AddToCart(product, quantity, context);
                if (addToCartResponse == null) return Results.InternalServerError("Failed to add product to cart.");

                return Results.Ok(addToCartResponse);

            });

            app.MapPut("/ShoppingCart/{ProductId}/{NewQuantity}", async (IShoppingCartService cartService, IRepository<ShoppingCartItems> cartItemRepo, IRepository<ShoppingCart> cartRepo, IRepository<Product> productRepo, int productId, int newQuantity, HttpContext context) =>
            {
                // include validation to see if newQuantity is allowed within the stock available.
                // find a way to abstract out the operations using dbContext

                if (newQuantity <= 0) return Results.BadRequest("New quantity must be a non-negative integer higher than 0.");

                var userCart = await cartService.GetUserCartAsync(context);
                if (userCart == null) return Results.NotFound("No active shopping cart found for the user.");

                var userCartItems = await cartService.GetShoppingCartItemByIdAsync(context, userCart, productId);
                if (userCartItems == null) return Results.NotFound($"No item with ProductId {productId} found in the user's cart.");

                var product = await productRepo.GetByIdAsync(productId);

                userCart.TotalQuantity = userCart.TotalQuantity - userCartItems.Quantity + newQuantity;
                userCart.TotalPrice = userCart.TotalPrice - (userCartItems.Quantity * product.Price) + (newQuantity * product.Price);
                userCartItems.Quantity = newQuantity;

                await cartRepo.UpdateAsync(userCart);

                return await cartItemRepo.UpdateAsync(userCartItems) > 0 ? Results.Ok("Cart item updated successfully.")
                    : Results.InternalServerError("Failed to update cart item.");
            });

            app.MapDelete("/ShoppingCart/ClearCart/", async (IShoppingCartService cartService, IRepository<ShoppingCart> cartRepo, HttpContext context) =>
            {
                var userCart = await cartService.GetUserCartAsync(context);
                if (userCart == null) return Results.NotFound("No active shopping cart found for the user.");

                return await cartRepo.DeleteAsync(userCart) > 0 ? Results.Ok("Shopping cart cleared successfully.")
                    : Results.InternalServerError("Failed to clear shopping cart.");

            });

            app.MapDelete("/ShoppingCart/{ProductId}", async (IShoppingCartService cartService, IRepository<ShoppingCartItems> cartItemRepo, IRepository<ShoppingCart> cartRepo, IRepository<Product> productRepo, int productId, HttpContext context) =>
            {
                var userCart = await cartService.GetUserCartAsync(context);
                if (userCart == null) return Results.NotFound("No active shopping cart found for the user.");

                var userCartItems = await cartService.GetShoppingCartItemByIdAsync(context, userCart, productId);
                if (userCartItems == null) return Results.BadRequest($"No item with ID {productId} in the cart.");

                var product = await productRepo.GetByIdAsync(productId);
                if (product == null) return Results.BadRequest($"Product Id {productId} does not exist.");

                if (userCart.TotalQuantity - userCartItems.Quantity == 0) return await cartRepo.DeleteAsync(userCart) > 0 ? Results.Ok("Item was removed successfully, and since it was the last one in the cart, the cart was removed.") : Results.InternalServerError("Failed to remove the item from the cart");

                userCart.TotalPrice -= product.Price * userCartItems.Quantity;
                userCart.TotalQuantity -= userCartItems.Quantity;

                await cartRepo.UpdateAsync(userCart);

                return await cartItemRepo.DeleteAsync(userCartItems) > 0 ? Results.Ok("Cart item deleted successfully.")
                    : Results.InternalServerError("Failed to delete cart item.");
            });

            app.MapPost("/ShoppingCart/Checkout", async (IShoppingCartService cartService, IRepository<ShoppingCart> cartRepo, HttpContext context) =>
            {
                var userCart = await cartService.GetUserCartAsync(context);
                if (userCart == null) return Results.NotFound("No active shopping cart found for the user.");

                var userCartItems = await cartService.GetShoppingCartItemsAsync(context, userCart);


                return await cartRepo.UpdateAsync(userCart) > 0 ? Results.Ok("Shopping cart checked out successfully.")
                    : Results.InternalServerError("Failed to checkout shopping cart.");
            });
        }
    }
}
