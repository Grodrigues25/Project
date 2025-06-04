namespace Project.Models
{
    public class Product
    {
        public required int ID { get; set; }
        public required string Name { get; set; }
        public required float Price { get; set; }
        public required int Stock { get; set; }
    }
}
