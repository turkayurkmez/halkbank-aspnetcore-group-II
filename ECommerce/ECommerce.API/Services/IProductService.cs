
using ECommerce.API.Models.Responses;
using ECommerce.Infrastructure.Entities;
using ECommerce.Infrastructure.Repositories;

namespace ECommerce.API.Services
{
    /*
     * Servisler bu mimaride artık yalnızca DTO (Response) döndürmeli.
     */
    public interface IProductService
    {
        Task<Product> CreateAsync(Product product);
        Task<IEnumerable<ProductListResponse>> GetAllProductsAsync();
        Task<ProductListResponse?> GetByIdAsync(int id);

        Task<bool> DeleteAsync(int id);
        Task<Product> UpdateAsync(Product product);
        Task<IEnumerable<ProductSummary>> GetProductSummaries();

        Task<Product> ApplyDiscount(int id, decimal discountRate);
    }
}