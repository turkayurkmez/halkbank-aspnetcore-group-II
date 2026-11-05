using ECommerce.API.Models;

namespace ECommerce.API.Services
{
    public interface IProductService
    {
        Task<Product> CreateAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetByIdAsync(int id);
    }
}