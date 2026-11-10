
using ECommerce.Infrastructure.Entities;
using ECommerce.Infrastructure.Repositories;

namespace ECommerce.API.Services
{
    public interface IProductService
    {
        Task<Product> CreateAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetByIdAsync(int id);

        Task<bool> DeleteAsync(int id);
        Task<Product> UpdateAsync(Product product);
        Task<IEnumerable<ProductSummary>> GetProductSummaries();

        Task<Product> ApplyDiscount(int id, decimal discountRate);
    }
}