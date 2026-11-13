

using ECommerce.Application.Models.Responses;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Repositories;

namespace ECommerce.Application.Services
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
    /*
     * Mimariyi böyle kurarsanız her yazılımcı, gelecekte buraya her davranış için bir fonksiyon yazacak. Yani bu class yatay büyüyecek yani şişmanlayacak!
     * 
     * Her özellik -> bir fonksyon olmasın
     * Her özellik -> nesne kombinasyonu.
     *  1. Parametre ne alır -> Request 
     *  2. Ne döndürür -> Response
     *  3. Ne/nasıl yapar -> Handler
     */
}