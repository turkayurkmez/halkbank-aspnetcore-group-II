using ECommerce.Infrastructure.Entities;
using System.Linq.Expressions;

namespace ECommerce.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>>? predicate = null);
        Task<T> GetbyIdAsync(int id);

        void UpdateAsync(T entity);
        void DeleteAsync(T entity);

        Task<bool> IsExistsAsync(int id);
    }
}
