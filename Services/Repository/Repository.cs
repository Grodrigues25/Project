using Microsoft.EntityFrameworkCore;
using Project.Services.Repository;

namespace Project.Services.UserManagementService
{
    public class Repository<TEntity>(UserDbContext dbContext) : IRepository<TEntity> where TEntity : class
    {
        public async Task<int> AddAsync(TEntity entity)
        {
            await dbContext.AddAsync(entity);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await dbContext.Set<TEntity>().ToListAsync(); 
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await dbContext.Set<TEntity>().FindAsync(id);
        }
    }
}
