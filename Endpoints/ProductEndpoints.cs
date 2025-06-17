using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Services;
using Project.Services.UserManagementService;

namespace Project.Endpoints
{
    public static class ProductEndpoints
    {
        public static void RegisterProductEndpoints(this WebApplication app)
        {
            app.MapGet("/Product", async (IRepository<Product> productRepo) =>
            {
                return await productRepo.GetAsync();
            });

            app.MapGet("/Product/{ProductId}", async (IRepository<Product> productRepo, int productId) =>
            {
                return await productRepo.GetByIdAsync(productId);
            });

            app.MapPost("/Product", async (IRepository<Product> productRepo, Product newProduct) =>
            {
                return await productRepo.AddAsync(newProduct);
            });

            app.MapDelete("/Product/{ProductId}", async (IRepository<Product> productRepo, int productId) =>
            {
                Product product = await productRepo.GetByIdAsync(productId);
                return await productRepo.DeleteAsync(product); 
            });
        }
    }
}
