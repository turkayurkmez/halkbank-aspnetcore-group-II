using ECommerce.Application.Features.Products.Queries.GetProducts;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace ECommerce.IntegrationTests
{
    public class ProductsEndPointTests (CustomWebApplicationFactory factory): IClassFixture<CustomWebApplicationFactory>
    {

        [Fact]
        public async Task GetProducts_ReturnsSuccessStatusCode()
        {

            // Arrange
            await factory.SeedAsync();
            var client = factory.CreateClient();
            // Act
            var response = await client.GetAsync("/api/products");
            // Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            //üç ürün geliyor mu?
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<GetProductResponse>>();
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            //result.Should()
        }

        [Fact]
        public async Task CreateProduct_WithoutToken_Returns401()
        {
            await factory.SeedAsync();
            var client = factory.CreateClient();
            var response = await client.PostAsJsonAsync("api/products", new { name = "Test", sku = "ELK-004", price = 1, stock = 1, categoryid = 1 });

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

    }
}
