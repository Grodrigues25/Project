using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    [PrimaryKey(nameof(CartId))]
    public class ShoppingCart
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }
        public int UserId { get; set; } 
        public int ProductId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
