using Project.Models.Products;

namespace Project.Services.Database
{
    public interface IDatabaseService
    {
        Task<List<Product>> FullTextSearch(string searchTerm);
    }
}
