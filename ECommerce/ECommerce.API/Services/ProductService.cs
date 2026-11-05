using ECommerce.API.Models;
using ECommerce.API.Repositories;
using ECommerce.API.Store;

namespace ECommerce.API.Services
{
    public class ProductService : IProductService
    // Bu sınıfın sorumluluğu, product nesnesiyle ilgili TÜM işlemleri yapmak.
    {
        private IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await productRepository.GetAllAsync();
        }


        public async Task<Product?> GetByIdAsync(int id)
        {

            await Task.CompletedTask;
            return null;
            // return _store.Products.FirstOrDefault(p => p.Id == id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await Task.CompletedTask;
            //  _store.AddProduct(product);
            return product;
        }

    }
}
