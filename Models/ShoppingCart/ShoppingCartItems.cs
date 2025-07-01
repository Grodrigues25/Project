using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.ShoppingCart
{
    [PrimaryKey(nameof(ArbitraryKeyForTracking))]
    public class ShoppingCartItems
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArbitraryKeyForTracking { get; set; }
        public required int CartId { get; set; }
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
    }
}
