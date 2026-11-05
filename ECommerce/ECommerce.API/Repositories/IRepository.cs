namespace ECommerce.API.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetbyIdAsync(int id);

        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int id);

    }
}
