using ECommerce.API.Models.Requests;
using ECommerce.API.Models.Responses;
using ECommerce.Infrastructure.Entities;
using Mapster;

namespace ECommerce.API.Mappings
{
    /*
     * Sadece isim ve tip eşleşmesini otomatik yapmak size yetmiyorsa 
     * (Daha derin koleksiyonlar, yapılması gereken denetimler vs varsa),
     * Tüm eşleşme kurallarına müdahale etmem gerekiyorsa  config yaparız.
     * 
     * Varsayılan: kaynak ve hedefte aynı isimde ve tipde özellikler eşleşir.
     * Bu size yetmiyorsa, buradasınız.
     */
    public class ProductMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Product, ProductListResponse>()
                  .Map(dest => dest.CategoryName, src => src.Category != null ? src.Category.Name : string.Empty);

            config.NewConfig<CreateProductRequest, Product>()
                  .Ignore(dest => dest.Id, dest => dest.Reviews, dest => dest.Category!, dest => dest.OrderItems);

        }
    }
}
