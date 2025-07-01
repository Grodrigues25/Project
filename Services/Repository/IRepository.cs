//https://www.youtube.com/watch?v=EvD8BU9HHWc

namespace Project.Services.Repository
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync();

        Task<TEntity> GetByIdAsync(int id);

        Task<int> AddAsync(TEntity entity);

        Task<int> DeleteAsync(TEntity entity);

        Task<int> UpdateAsync(TEntity entity);
    }
}
