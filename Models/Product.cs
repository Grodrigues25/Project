using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models

{
    [PrimaryKey(nameof(ProductId))]
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public required float Price { get; set; }
        public required int Stock { get; set; }
        public required string Description { get; set; }
        public required string Category { get; set; }
    }
}
