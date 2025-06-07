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
                return context.product.ToList();
            });

            app.MapGet("/Product/{ProductId}", (UserDbContext context, int ProductId) =>
            {
                var userList = context.product.ToList();
                var specificProduct = userList.FirstOrDefault(p => p.ProductId == ProductId);

                return specificProduct is not null ? Results.Ok(specificProduct) : Results.NotFound($"Item with ID {ProductId} not found.");
            });

            app.MapPost("/Product", (Product newProduct, UserDbContext context) =>
            {
                context.Add(newProduct);
                context.SaveChanges();
            });

        }
    }
}
