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


        public async Task<Product?> GetByIdAsync(int id) {

            await Task.CompletedTask;
            return _store.Products.FirstOrDefault(p => p.Id == id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await Task.CompletedTask;
            _store.AddProduct(product);
            return product;
        }

    }
}
