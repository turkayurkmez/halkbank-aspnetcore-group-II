using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories
{
    public class EFProductRepository(AppDbContext db) : IProductRepository
    {


        public async Task<Product> CreateAsync(Product entity)
        {
            await db.Products.AddAsync(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await db.Products.FindAsync(id);
            if (existing is null)
            {
                return false;
            }
            db.Products.Remove(existing);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await db.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product?> GetbyIdAsync(int id)
        {
            return await db.Products.AsNoTracking().Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            return await db.Products.AnyAsync(p => p.Id == id);

        }

        public async Task<IEnumerable<Product>> SearchByNameAsync(string name)
        {
            return await db.Products.AsNoTracking().Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            db.Products.Update(entity); //entity'nin id'si 0 ise INSERT pozitif sayı ise UPDATE kullanır!!!

            //yukarıdaki satır olmasaydı:
            //var existing = await db.Products.FindAsync(entity.Id);
            //existing.Name = entity.Name;
            //existing.SKU = entity.SKU;
            //existing.Price = entity.Price;


            await db.SaveChangesAsync();




            return entity;

        }

        //public async Task Test()
        //{
        //    var result = await db.Orders.Include(o => o.Items).ThenInclude(i => i.Product).ToListAsync();
        //}

        public async Task<IEnumerable<ProductSummary>> GetProductSummaries()
        {
            return db.Products.OrderByDescending(p => p.Id)
                                        .Select(p => new ProductSummary(Id: p.Id, Name: p.Name, Price: p.Price, CategoryName: p.Category.Name));
        }

        public async Task<Product> ApplyDiscountAsync(int id, decimal discountRate)
        {
            var product = await db.Products.FindAsync(id);
            product.Price *= 1 - discountRate;

            await db.SaveChangesAsync();
            return product;
        }
    }

    public record ProductSummary(int Id, string Name, decimal Price, string CategoryName);
}
