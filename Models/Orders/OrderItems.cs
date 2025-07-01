using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Orders
{
    [PrimaryKey(nameof(EFKeyForOrderItems))]
    public class OrderItems
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EFKeyForOrderItems { get; set; }

        required public int OrderId { get; set; }

        required public int ProductId { get; set; }

        required public int Quantity { get; set; }
    }
}
