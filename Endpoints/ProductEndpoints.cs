using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Services;
using Project.Services.Repository;
using System.Reflection.Metadata.Ecma335;

namespace Project.Endpoints
{
    public static class ProductEndpoints
    {
        public static void RegisterProductEndpoints(this WebApplication app)
        {
            app.MapGet("/Product", async (IRepository<Product> productRepo) =>
            {
                var productList = await productRepo.GetAsync();
                return Results.Ok(productList);
            });

            app.MapGet("/Product/{ProductId}", async (IRepository<Product> productRepo, int productId) =>
            {
                if (productId < 0)
                {
                    return Results.BadRequest("Product ID need to be a positive integer");
                }

                var product = await productRepo.GetByIdAsync(productId);
                return product != null ? Results.Ok(product) : Results.NotFound($"There is no product with ID {productId}.");                
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
