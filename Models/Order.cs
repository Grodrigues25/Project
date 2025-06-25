using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Order
    {
        public required int OrderId { get; set; }
        public required int UserId { get; set; }
        public required float TotalPrice { get; set; }
        public required string Status { get; set; }
        public required string Date { get; set; }
    }
}
