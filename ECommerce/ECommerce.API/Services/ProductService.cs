using ECommerce.API.Data;
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

            return await productRepository.GetbyIdAsync(id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await Task.CompletedTask;
            //  _store.AddProduct(product);
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            //id'nin varlık kontrolünü repository yapıyor:
            return await productRepository.DeleteAsync(id);
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            return await productRepository.UpdateAsync(product);

        }

        
        public async Task<IEnumerable<ProductSummary>> GetProductSummaries()
        {
            return await productRepository.GetProductSummaries();
        }

        public async Task<Product> ApplyDiscount(int id, decimal discountRate)
        {
            return await productRepository.ApplyDiscountAsync(id, discountRate);
        }
    }

   



}
