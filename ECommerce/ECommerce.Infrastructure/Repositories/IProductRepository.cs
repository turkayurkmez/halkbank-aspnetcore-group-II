using ECommerce.Infrastructure.Entities;

namespace ECommerce.Infrastructure.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<ProductSummary>> GetProductSummaries();
        Task<IEnumerable<Product>> SearchByNameAsync(string name);

        Task<Product> ApplyDiscountAsync(int id, decimal discountRate);


    }
}
