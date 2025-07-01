using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Orders
{
    [PrimaryKey(nameof(EFKeyForOrderItems))]
    public class OrderItems
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EFKeyForOrderItems { get; set; }
        public required int OrderId { get; set; }
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
    }
}
