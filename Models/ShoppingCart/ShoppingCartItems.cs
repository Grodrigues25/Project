using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.ShoppingCart
{
    [PrimaryKey(nameof(ArbitraryKeyForTracking))]
    public class ShoppingCartItems
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArbitraryKeyForTracking { get; set; }

        required public int CartId { get; set; }

        required public int ProductId { get; set; }

        required public int Quantity { get; set; }
    }
}
