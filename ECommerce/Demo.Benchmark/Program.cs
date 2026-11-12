using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ECommerce.API.Models.Responses;
using ECommerce.Infrastructure.Entities;
using Mapster;

BenchmarkRunner.Run<MappingBenchmarks>();

[MemoryDiagnoser]
public class MappingBenchmarks
{
    private List<Product> _products = default!;
    private TypeAdapterConfig _config = default!;

    [Params(1, 1000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _config = new TypeAdapterConfig();
        _config.NewConfig<Product, ProductListResponse>()
               .Map(d => d.Sku, s => s.SKU)
               .Map(d => d.CategoryName, s => s.Category != null ? s.Category.Name : string.Empty);

        _products = Enumerable.Range(1, Count).Select(i => new Product
        {
            Id = i,
            Name = $"Product {i}",
            SKU = $"SKU-{i}",
            Price = i * 1.5m,
            Stock = i % 50,
            CategoryId = 1,
            Category = new Category { Id = 1, Name = "Category" }
        }).ToList();
    }

    [Benchmark(Baseline = true)]
    public List<ProductListResponse> Manual()
    {
        var result = new List<ProductListResponse>(_products.Count);
        foreach (var p in _products)
        {
            result.Add(new ProductListResponse(p.Id, p.Name, p.SKU, p.Price, p.Stock,
                p.Category != null ? p.Category.Name : string.Empty));
        }
        return result;
    }

    [Benchmark]
    public List<ProductListResponse> MapsterAdapt() => _products.Adapt<List<ProductListResponse>>(_config);
}
