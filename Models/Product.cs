using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models

{
    [PrimaryKey(nameof(ProductId))]
    [Index(nameof(Name), nameof(Description), nameof(Category))]
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Name is required"), MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "Price is required"), Range(0.01, 10000, ErrorMessage = "Price needs to be a value between 1 cent and 10000€")]
        public required float Price { get; set; }
        [Required(ErrorMessage = "Stock is required"), Range(0, 1000, ErrorMessage = "Stock needs to be a non-negative integer with a maximum of 1000")]
        public required int Stock { get; set; }
        [Required(ErrorMessage = "Description is required"), MinLength(10, ErrorMessage = "Description must be at least 10 characters long")]
        public required string Description { get; set; }
        [Required(ErrorMessage = "Category is required"), MinLength(3, ErrorMessage = "Category must be at least 3 characters long")]
        public required string Category { get; set; }
    }
}
