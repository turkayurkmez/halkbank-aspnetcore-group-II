using ECommerce.API.Models;
using ECommerce.API.Store;

namespace ECommerce.API.Services
{
    public class ProductService // Bu sınıfın sorumluluğu, product nesnesiyle ilgili TÜM işlemleri yapmak.
    {
        private InMemoryStore _store;

        public ProductService(InMemoryStore store)
        {
            _store = store;
        }

        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        {
            return _store.Products;
        }


    }
}
