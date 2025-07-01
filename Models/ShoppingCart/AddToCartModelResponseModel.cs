using Project.Models.Products;
using System.ComponentModel.DataAnnotations;

namespace Project.Models.ShoppingCart
{
    public class AddToCartModelResponseModel
    {
        [Required(ErrorMessage = "CustomerId is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "ProductId is required.")]
        public Product ProductToAdd { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "TotalPrice is required.")]
        public float TotalPrice { get; set; }

    }
}
