

using ECommerce.API.Models.Responses;
using ECommerce.Infrastructure.Entities;
using ECommerce.Infrastructure.Repositories;
using Mapster;

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

        public async Task<IEnumerable<ProductListResponse>> GetAllProductsAsync()
        {
            var products = await uow.Products.ListWithCategoryAsync();

            //return products.Select(p => new ProductListResponse(
            //    Id: p.Id,
            //    Name: p.Name,
            //    Sku: p.SKU,
            //    Price: p.Price,
            //    Stock: p.Stock,
            //    CategoryName: p.Category?.Name ?? string.Empty));

            return products.Adapt<IEnumerable<ProductListResponse>>();
        }


        public async Task<ProductListResponse?> GetByIdAsync(int id)
        {

            var product = await uow.Products.GetbyIdAsync(id);
            return product.Adapt<ProductListResponse>();
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
