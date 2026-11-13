namespace ECommerce.Application.Models.Responses
{
    public record ProductListResponse(int Id, string Name, string Sku, decimal Price, int Stock, string CategoryName);

    public record CategoryResponse(int Id, string Name);
   
}
