using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.ShoppingCart
{
    [PrimaryKey(nameof(CartId))]
    public class ShoppingCart
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }
        public int UserId { get; set; } 
        public required float TotalPrice { get; set; }
        public required int TotalQuantity { get; set; }
        public bool isCheckedOut { get; set; } = false;
    }
}
