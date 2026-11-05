using ECommerce.API.Models;

namespace ECommerce.API.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> SearchByNameAsync(string name);

    }
}
