using ECommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            await db.Database.MigrateAsync();

            if (!await db.Categories.AnyAsync())
            {
                var electonics = new Category { Name = "Elektronik" };
                var clothing = new Category { Name = "Giyim" };
                var books = new Category { Name = "Kitap" };

                

                db.Categories.AddRange(electonics, clothing, books);
                await db.SaveChangesAsync();


                db.Products.AddRange(

                    new Product { Name = "Kulaklık", Price = 2000, SKU = "ELK-001", CategoryId = electonics.Id },

                    new Product { Name = "Tişört", Price = 500, SKU = "GYM-001", CategoryId = clothing.Id },
                    new Product { Name = "ASP.net core", Price = 2000, SKU = "KTP-001", CategoryId = books.Id }


                 );


            }

            if (!await db.Customers.AnyAsync())
            {
                db.Customers.AddRange(new Customer { FullName = "Necati Temiz", Email = "necati@gmail.com" });
                db.Customers.AddRange(new Customer { FullName = "İpek Turgut", Email = "ipek.turgut@gmail.com" });

            }

            await db.SaveChangesAsync();
        }
    }
}
