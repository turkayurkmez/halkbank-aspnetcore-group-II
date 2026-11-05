using ECommerce.API.Models;
using ECommerce.API.Store;

namespace ECommerce.API.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {

        private readonly InMemoryStore _store;

        public InMemoryProductRepository(InMemoryStore store)
        {
            _store = store;
        }

        public Task<Product> CreateAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<Product> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            await Task.CompletedTask;
            return _store.Products;
        }

        public Task<Product> GetbyIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> SearchByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
