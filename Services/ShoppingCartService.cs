using Project.Services.Repository;
using Project.Models;
using Project.Models.ShoppingCart;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Project.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly UserDbContext _dbcontext;

        public ShoppingCartService(UserDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<AddToCartModelResponseModel> AddToCart(IRepository<ShoppingCart> cartRepo, IRepository<ShoppingCartItems> cartItemsRepo, Product product, int quantity, HttpContext context)
        {
            var userClaims = context.User.Identity as ClaimsIdentity;
            var userId = int.Parse(userClaims.FindFirst("id").Value);

            var userCart = await _dbcontext.shoppingCarts
                .Where(cart => cart.UserId == userId && !cart.isCheckedOut)
                .FirstOrDefaultAsync();

            if (userCart == null)
            {
                ShoppingCart newUserCart = new ShoppingCart
                {
                    UserId = userId,
                    TotalPrice = product.Price * quantity,
                    TotalQuantity = quantity,
                    isCheckedOut = false
                };

                await cartRepo.AddAsync(newUserCart);
            }

            userCart = await _dbcontext.shoppingCarts
                .Where(cart => cart.UserId == userId && !cart.isCheckedOut)
                .FirstOrDefaultAsync();

            userCart.TotalPrice += product.Price * quantity;
            userCart.TotalQuantity += quantity;

            var userCartItem = new ShoppingCartItems()
            {
                CartId = userCart.CartId,
                ProductId = product.ProductId,
                Quantity = quantity,
            };

            await cartItemsRepo.AddAsync(userCartItem);
            
            return new AddToCartModelResponseModel
            {
                UserId = userId,
                ProductToAdd = product,
                Quantity = quantity,
                TotalPrice = userCart.TotalPrice,
            };
        }
    }
}
