using Microsoft.EntityFrameworkCore;
using Project.Models.Authentication;
using Project.Models.Orders;
using Project.Models.Products;
using Project.Models.ShoppingCart;
using Project.Models.Users;

// https://learn.microsoft.com/en-us/azure/azure-sql/database/azure-sql-dotnet-entity-framework-core-quickstart?view=azuresql&tabs=dotnet-cli%2Cservice-connector%2Cportal

namespace Project.Services.Database
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> user { get; set; }

        public DbSet<Product> product { get; set; }

        public DbSet<BlacklistModel> blacklist { get; set; }

        public DbSet<Order> order { get; set; }

        public DbSet<OrderItems> orderItems { get; set; }

        public DbSet<ShoppingCart> shoppingCarts { get; set; }

        public DbSet<ShoppingCartItems> shoppingCartItems { get; set; }

        // https://learn.microsoft.com/en-us/ef/core/modeling/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // https://learn.microsoft.com/en-us/ef/core/modeling/generated-properties?tabs=data-annotations

            modelBuilder.Entity<User>()
                .HasIndex(user => user.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany<Order>()
                .WithOne()
                .HasForeignKey(order => order.UserId)
                .IsRequired();

            // Order Items Constraints
            modelBuilder.Entity<Order>()
                .HasMany<OrderItems>()
                .WithOne()
                .HasForeignKey(orderItem => orderItem.OrderId)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .HasMany<OrderItems>()
                .WithOne()
                .HasForeignKey(orderItem => orderItem.ProductId)
                .IsRequired();

            // Shopping Cart Constraints
            modelBuilder.Entity<User>()
                .HasMany<ShoppingCart>()
                .WithOne()
                .HasForeignKey(ShoppingCartItem => ShoppingCartItem.UserId)
                .IsRequired();

            // Shopping Cart Items Constraints
            modelBuilder.Entity<ShoppingCart>()
                .HasMany<ShoppingCartItems>()
                .WithOne()
                .HasForeignKey(cartItem => cartItem.CartId)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .HasMany<ShoppingCartItems>()
                .WithOne()
                .HasForeignKey(cartItem => cartItem.ProductId)
                .IsRequired();
        }
    }
}
