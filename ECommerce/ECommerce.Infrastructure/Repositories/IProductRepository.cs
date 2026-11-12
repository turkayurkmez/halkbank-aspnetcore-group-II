using ECommerce.Infrastructure.Entities;

namespace ECommerce.Infrastructure.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<ProductSummary>> GetProductSummaries();
        Task<IEnumerable<Product>> SearchByNameAsync(string name);

        Task<Product> ApplyDiscountAsync(int id, decimal discountRate);

        Task<bool> IsSKUExists(string sku);

        //Include fonksiyonunu çağıracağını sade EFProductRepository nesnesi bilir.
        Task<IEnumerable<Product>> ListWithCategoryAsync();
        Task<Product> GetProductWithCategory(int id);
        



    }
}
