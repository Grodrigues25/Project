using Project.Models;
using Project.Models.ShoppingCart;
using Project.Services.Repository;

namespace Project.Services
{
    public interface IShoppingCartService
    {
        Task<AddToCartModelResponseModel> AddToCart(IRepository<ShoppingCart> cartRepo, IRepository<ShoppingCartItems> cartItemsRepo, Product product, int quantity, HttpContext context);
    }
}
