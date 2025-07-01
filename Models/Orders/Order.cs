using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Orders
{
    [PrimaryKey(nameof(OrderId))]
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        required public int UserId { get; set; }

        [Required(ErrorMessage = "Total price is required")]
        [Range(0.01, 100000, ErrorMessage = "Total price needs to be a value between 1 cent and 100000€")]
        required public float TotalPrice { get; set; }

        [Required(ErrorMessage = "Status is required")]
        required public string Status { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
