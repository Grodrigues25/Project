using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Models.ShoppingCart;
using Project.Services;
using Project.Services.Repository;

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

            app.MapPut("/ShoppingCart/{ProductId}/{NewQuantity}", async (IShoppingCartService cartService, IRepository<ShoppingCartItems> cartItemRepo, IRepository<ShoppingCart> cartRepo, IRepository<Product> productRepo, int productId, int newQuantity, HttpContext context) => {

                if (newQuantity <= 0) return Results.BadRequest("New quantity must be a non-negative integer higher than 0.");

                var userCart = await cartService.GetUserCartAsync(context);
                if (userCart == null) return Results.NotFound("No active shopping cart found for the user.");

                var userCartItems = await cartService.GetShoppingCartItemByIdAsync(context, userCart, productId);
                if (userCartItems == null) return Results.NotFound($"No item with ProductId {productId} found in the user's cart.");

                var product = await productRepo.GetByIdAsync(productId);
                if (product.Stock - newQuantity < 0) return Results.BadRequest($"Product Id {productId} does not have enough stock. Available stock: {product.Stock}");

                userCart.TotalQuantity = userCart.TotalQuantity - userCartItems.Quantity + newQuantity;
                userCart.TotalPrice = userCart.TotalPrice - (userCartItems.Quantity * product.Price) + (newQuantity * product.Price);
                userCartItems.Quantity = newQuantity;

                await cartRepo.UpdateAsync(userCart);

                return await cartItemRepo.UpdateAsync(userCartItems) > 0 ? Results.Ok("Cart item updated successfully.")
                    : Results.InternalServerError("Failed to update cart item.");
            });

            app.MapDelete("/ShoppingCart/ClearCart/", async (IShoppingCartService cartService, IRepository<ShoppingCart> cartRepo, HttpContext context) => {

                var userCart = await cartService.GetUserCartAsync(context);
                if (userCart == null) return Results.NotFound("No active shopping cart found for the user.");

                return await cartRepo.DeleteAsync(userCart) > 0 ? Results.Ok("Shopping cart cleared successfully.")
                    : Results.InternalServerError("Failed to clear shopping cart.");

            });

            app.MapDelete("/ShoppingCart/{ProductId}", async (IShoppingCartService cartService, IRepository<ShoppingCartItems> cartItemRepo, IRepository<ShoppingCart> cartRepo, IRepository<Product> productRepo, int productId, HttpContext context) => {

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

            app.MapPost("/ShoppingCart/Checkout", async (IShoppingCartService cartService, IRepository<ShoppingCart> cartRepo, IRepository<Order> orderRepo, IRepository<Product> productRepo, HttpContext context, UserDbContext dbcontext) => {

                var userCart = await cartService.GetUserCartAsync(context);
                if (userCart == null) return Results.NotFound("No active shopping cart found for the user.");

                var userCartItems = await cartService.GetShoppingCartItemsAsync(context, userCart);

                foreach (var item in userCartItems)
                {
                    var product = await productRepo.GetByIdAsync(item.ProductId);

                    if (product.Stock - item.Quantity < 0) return Results.BadRequest($"Product with ID {item.ProductId} does not have enough stock. Available stock: {product.Stock}");

                    product.Stock -= item.Quantity;
                    await productRepo.UpdateAsync(product);
                }

                Order newOrder = new Order()
                {
                    UserId = userCart.UserId,
                    TotalPrice = userCart.TotalPrice,
                    Status = "Pending"
                };

                await orderRepo.AddAsync(newOrder);
                var userOrders = await dbcontext.order.Where(o => o.UserId == userCart.UserId).ToListAsync();
                var newOrderInDb = userOrders[userOrders.Count - 1];

                foreach (var product in userCartItems)
                {
                    var orderedProduct = new OrderItems()
                    {
                        OrderId = newOrderInDb.OrderId,
                        ProductId = product.ProductId,
                        Quantity = product.Quantity
                    };

                    var addingItemToOrderResult = await dbcontext.orderItems.AddAsync(orderedProduct);
                    if (addingItemToOrderResult == null) return Results.InternalServerError("Failed to add item to order.");
                }

                return await cartRepo.DeleteAsync(userCart) > 0 ? Results.Created()
                    : Results.InternalServerError("Failed to clear shopping cart after checkout.");
            });
        }
    }
}
