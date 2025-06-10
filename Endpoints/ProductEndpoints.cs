using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Services;

namespace Project.Endpoints
{
    public static class ProductEndpoints
    {
        public static void RegisterProductEndpoints(this WebApplication app)
        {
            app.MapGet("/Product", (UserDbContext context) =>
            {
                return context.product.ToList() == null ?  Results.NotFound("No products in existence") : Results.Ok(context.product.ToList());
            });

            app.MapGet("/Product/{ProductId}", (UserDbContext context, int ProductId) =>
            {
                var productList = context.product.ToList();
                var specificProduct = productList.FirstOrDefault(p => p.ProductId == ProductId);

                return specificProduct is not null ? Results.Ok(specificProduct) : Results.NotFound($"Item with ID {ProductId} not found.");
            });

            app.MapPost("/Product", (Product newProduct, UserDbContext context) =>
            {
                context.Add(newProduct);
                context.SaveChanges();
            });

            app.MapDelete("/Product/{ProductId}", (UserDbContext context, int ProductId) =>
            {
                return context.product.Where(c => c.ProductId == ProductId).ExecuteDelete() > 0 ? Results.Ok($"Product with ID {ProductId} was successfully deleted") : Results.NotFound("Product ID specific does not exist");

                //return specificProduct is not null ? Results.Ok(specificProduct) : Results.NotFound($"Item with ID {ProductId} not found.");
            });
        }
    }
}
