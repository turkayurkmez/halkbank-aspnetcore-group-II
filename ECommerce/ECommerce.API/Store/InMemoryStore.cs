using ECommerce.API.Models;

namespace ECommerce.API.Store
{
    public class InMemoryStore // Bu sınıfın sorumluluğu bellekte product nesnesini tutmak.
    {
        private int lastProductId=0;
        public List<Product> Products { get; } = new();

        public void AddProduct(Product product)
        {
            product.Id = nextProductId();
            Products.Add(product);
        }

        private int nextProductId()
        {
            return ++lastProductId;
        }

        public InMemoryStore()
        {
            AddProduct(new Product { Name = "TV Sehpası", Price = 3000, CategoryId = 1 });
            AddProduct(new Product { Name = "Kulaklık", Price = 2000, CategoryId = 2 });
            AddProduct(new Product { Name = "Tişört", Price = 500, CategoryId = 3 });


        }
    }
}
