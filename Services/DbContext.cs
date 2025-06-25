using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Models.Authentication;

// https://learn.microsoft.com/en-us/azure/azure-sql/database/azure-sql-dotnet-entity-framework-core-quickstart?view=azuresql&tabs=dotnet-cli%2Cservice-connector%2Cportal

namespace Project.Services
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

        // https://learn.microsoft.com/en-us/ef/core/modeling/
        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // https://learn.microsoft.com/en-us/ef/core/modeling/generated-properties?tabs=data-annotations

            modelBuilder.Entity<User>()
                .HasIndex(b => b.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany<Order>()
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired();
        }  
        #endregion
    }
}
