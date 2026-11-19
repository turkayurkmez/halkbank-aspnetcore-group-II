using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using ECommerce.Application.Features.Users.Commands.RegisterUser;

namespace ECommerce.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {

        private readonly string _databaseName = $"ECommerceTests-{Guid.NewGuid()}";
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            builder.UseEnvironment("IntegrationTest");
            builder.UseSetting("Jwt:SigninKey", "test-signing-key-en-az-32-karakter-1234567890");

            builder.ConfigureServices(services =>
            {
                // Test ortamı için gerekli servisleri burada yapılandırabilirsiniz.
                // Örneğin, test veritabanı bağlantısı veya mock servisler ekleyebilirsiniz.

                //AddDbContext'in bıraktığı HER şeyi kaldırıyoruz. Çünkü test ortamında farklı bir veritabanı kullanacağız.
                //EF Core 10 seçenek ayarlarını ayrı bir IDbContextOptionsConfiguration kaydında tutuyor. Bu yüzden sadece AppDbContext ile ilgili olanları kaldırıyoruz.

                var toRemove = services.
                                  Where(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>)
                                          || d.ServiceType == typeof(AppDbContext)
                                          || d.ServiceType.IsGenericType && d.ServiceType.Name.StartsWith("IDbContextOptionsConfiguration") && d.ServiceType.GenericTypeArguments[0] == typeof(AppDbContext)
                                      ).ToList();


                foreach (var descriptor in toRemove)
                {
                    services.Remove(descriptor);
                }


                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase(_databaseName);
                });
            });
        }

        public async Task SeedAsync(int productCount = 3)
        {
            //in memory db içine test verilerini ekle:

            using var scope = Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dbContext.Database.EnsureCreatedAsync();

            if (await dbContext.Categories.AnyAsync())
            {
                return;
            }

            var category = new ECommerce.Domain.Entities.Category
            {
                Name = "Elektronik"
            };

            dbContext.Categories.Add(category);

            await dbContext.SaveChangesAsync();

            for (int i = 0; i < productCount; i++)
            {
                dbContext.Products.Add(new ECommerce.Domain.Entities.Product
                {
                    Name = $"Test Ürünü {i + 1}",
                    Price = 100 + i * 10,
                    CategoryId = category.Id
                });
            }

            await dbContext.SaveChangesAsync();

        }

        //token almak için testlerde kullanacağımız bir yardımcı metot:
        public async Task<HttpClient> GetAuthenticatedClientAsync(string email="test@example.com")
        {

            var client = CreateClient();
          
            var response = await client.PostAsJsonAsync("/api/auth/register", new { Email = email, Password = "Test@123", fullname="test kullanıcı", birthdate="1990-01-01" });
            response.EnsureSuccessStatusCode();

            var auth = await response.Content.ReadFromJsonAsync<AuthResponse>();

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {auth?.AcceesToken}");

            return client;
        }


    }
}
