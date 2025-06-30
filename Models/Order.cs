using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    [PrimaryKey(nameof(OrderId))]
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        [Required(ErrorMessage = "User ID is required")]
        public required int UserId { get; set; }
        [Required(ErrorMessage = "Total price is required"), Range(0.01, 100000, ErrorMessage = "Total price needs to be a value between 1 cent and 100000€")]
        public required float TotalPrice { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public required string Status { get; set; }
        Add commentMore actions
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
