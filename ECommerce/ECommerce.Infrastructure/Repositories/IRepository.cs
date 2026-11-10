namespace ECommerce.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetbyIdAsync(int id);

        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);

        Task<bool> IsExistsAsync(int id);
    }
}
