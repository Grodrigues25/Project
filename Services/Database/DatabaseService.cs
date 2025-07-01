using Microsoft.EntityFrameworkCore;
using Project.Models.Products;

namespace Project.Services.Database
{
    public class DatabaseService : IDatabaseService
    {
        UserDbContext _dbcontext;

        public DatabaseService(UserDbContext context)
        {
            _dbcontext = context;
        }

        public async Task<List<Product>> FullTextSearch(string searchTerm)
        {
            var results = await _dbcontext.product.Where(p => EF.Functions.FreeText(p.Name, searchTerm) || EF.Functions.FreeText(p.Description, searchTerm) || EF.Functions.FreeText(p.Category, searchTerm)).ToListAsync();
            return results;
        }
    }
}
