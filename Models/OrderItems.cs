using Microsoft.EntityFrameworkCore;

namespace Project.Models
{
    [Keyless]
    public class OrderItems
    {
        public required int OrderId { get; set; }
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
    }
}
