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
                //var ProductList = await context.product.ToListAsync();
                //return ProductList == null ?  Results.NotFound("No products in existence") : Results.Ok(ProductList);
            });

            app.MapGet("/Product/{ProductId}", async (IRepository<Product> productRepo, int productId) =>
            {
                return await productRepo.GetByIdAsync(productId);
                //var productList = await context.product.ToListAsync();
                //var specificProduct = productList.FirstOrDefault(p => p.ProductId == productId);

                //return specificProduct is not null ? Results.Ok(specificProduct) : Results.NotFound($"Item with ID {productId} not found.");
            });

            app.MapPost("/Product", async (IRepository<Product> productRepo, Product newProduct) =>
            {
                return await productRepo.AddAsync(newProduct);
                //await context.AddAsync(newProduct);
                //await context.SaveChangesAsync();
                //return Results.Created();
            });

            app.MapDelete("/Product/{ProductId}", async (IRepository<Product> productRepo, int productId) =>
            {
                Product product = await productRepo.GetByIdAsync(productId);
                return await productRepo.DeleteAsync(product);
                //return await context.product.Where(c => c.ProductId == productId).ExecuteDeleteAsync() > 0 ? Results.Ok($"Product with ID {productId} was successfully deleted") : Results.NotFound("Product ID specific does not exist");
 
            });
        }
    }
}
