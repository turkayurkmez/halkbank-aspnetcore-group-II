

using ECommerce.Infrastructure.Entities;
using ECommerce.Infrastructure.Repositories;

namespace ECommerce.API.Services
{
    public class ProductService(IUnitOfWork uow) : IProductService
    // Bu sınıfın sorumluluğu, product nesnesiyle ilgili TÜM işlemleri yapmak.
    {
        //private IProductRepository productRepository;

        //public ProductService(IProductRepository productRepository)
        //{
        //    this.productRepository = productRepository;
        //}

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await uow.Products.GetAllAsync();
        }


        public async Task<Product?> GetByIdAsync(int id)
        {

            return await uow.Products.GetbyIdAsync(id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await uow.Products.CreateAsync(product);
            await uow.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            //id'nin varlık kontrolünü repository yapıyor:
            var product = await uow.Products.GetbyIdAsync(id);
            try
            {
                uow.Products.DeleteAsync(product);
                await uow.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }



        }

        public async Task<Product> UpdateAsync(Product product)
        {
            uow.Products.UpdateAsync(product);
            await uow.SaveChangesAsync();
            return product;

        }


        public async Task<IEnumerable<ProductSummary>> GetProductSummaries()
        {
            return await uow.Products.GetProductSummaries();
        }

        public async Task<Product> ApplyDiscount(int id, decimal discountRate)
        {
            var product = await uow.Products.ApplyDiscountAsync(id, discountRate);
            await uow.SaveChangesAsync();
            return product;
        }
    }





}
