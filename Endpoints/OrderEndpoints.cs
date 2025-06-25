using Project.Models;
using Project.Services.Repository;
using Project.Services;

namespace Project.Endpoints
{
    public static class OrderEndpoints
    {
        public static void RegisterOrderEndpoints(this WebApplication app)
        {
            app.MapGet("/Order", async (IRepository<Order> orderRepo) =>
            {
                var orderList = await orderRepo.GetAsync();
                return Results.Ok(orderList);
            }).RequireAuthorization("adminAccess");


            app.MapGet("/Order/{OrderId}", async (IRepository<Order> orderRepo, int orderId) =>
            {
                if (orderId < 0)
                {
                    return Results.BadRequest("Order ID needs to be a positive integer");
                }
                var order = await orderRepo.GetByIdAsync(orderId);
                return order != null ? Results.Ok(order) : Results.NotFound($"There is no order with ID {orderId}.");
            }).RequireAuthorization("adminAccess");


            app.MapPost("/Order", async (IRepository<Order> orderRepo, Order newOrder, IAuthenticationService auth, HttpRequest request) =>
            {
                await orderRepo.AddAsync(newOrder);
                return Results.Created($"/Order/{newOrder.OrderId}", newOrder);
            }).RequireAuthorization("adminAccess");


            app.MapPut("/Order/{OrderId}", async (IRepository<Order> orderRepo, Order updatedOrder, int orderId, IAuthenticationService auth, HttpRequest request) =>
            {
                bool tokenIsValid = await auth.ValidateJwtToken(request);
                if (!tokenIsValid) return Results.Unauthorized();

                if (orderId < 0)
                {
                    return Results.BadRequest("Order ID needs to be a positive integer");
                }
                if (updatedOrder.OrderId != orderId)
                {
                    return Results.BadRequest("Order ID in the body does not match the Order ID in the URL.");
                }

                await orderRepo.UpdateAsync(updatedOrder);
                return Results.NoContent();
            }).RequireAuthorization("adminAccess");


            app.MapDelete("/Order/{OrderId}", async (IRepository<Order> orderRepo, int orderId, IAuthenticationService auth, HttpRequest request) =>
            {
                bool tokenIsValid = await auth.ValidateJwtToken(request);
                if (!tokenIsValid) return Results.Unauthorized();

                if (orderId < 0)
                {
                    return Results.BadRequest("Order ID needs to be a positive integer");
                }

                Order order = await orderRepo.GetByIdAsync(orderId);
                await orderRepo.DeleteAsync(order);
                return Results.NoContent();

            }).RequireAuthorization("adminAccess");
        }
    }
}
