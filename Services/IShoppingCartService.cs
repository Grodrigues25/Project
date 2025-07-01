using Project.Models;
using Project.Models.ShoppingCart;

namespace Project.Services
{
    public interface IShoppingCartService
    {
        Task<AddToCartModelResponseModel> AddToCart(Product product, int quantity, HttpContext context);
        Task<ShoppingCart?> GetUserCartAsync(HttpContext context);
        Task<ShoppingCartItems?> GetShoppingCartItemsAsync (HttpContext context, ShoppingCart userCart);
    }
}
