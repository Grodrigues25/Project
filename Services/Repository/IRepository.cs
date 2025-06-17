using Project.Models;

//https://www.youtube.com/watch?v=EvD8BU9HHWc

namespace Project.Services.UserManagementService
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetByIdAsync(int id);
    }
}
