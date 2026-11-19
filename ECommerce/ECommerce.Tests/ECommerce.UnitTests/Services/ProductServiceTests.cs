using ECommerce.Application.Exceptions;
using ECommerce.Application.Services;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Repositories;
using FluentAssertions;
using MapsterMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UnitTests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IUnitOfWork> _uow = new();
        private readonly Mock<IProductRepository> _productRepository = new();
        private readonly Mock<IMapper> _mapper = new();

        private ProductService CreateInstance()
        {
            _uow.Setup(x => x.Products).Returns(_productRepository.Object);
            return new ProductService(_uow.Object);
        }

        [Fact]
        public async Task GetByIdAsync_When_MissingProduct_ThrowsNotFoundException()
        {
            // Arrange
            _productRepository.Setup(x => x.GetbyIdAsync(It.IsAny<int>())).ReturnsAsync((Product?)null);

            var act = async () => await CreateInstance().GetByIdAsync(56);

            await act.Should().ThrowAsync<NotFoundException>().WithMessage("Ürün bulunamadı");


        }

        [Fact]
        public async Task DeleteAsync_When_ProductExists_RemoveAndSaveChanges()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Test Product" };
            _productRepository.Setup(x => x.GetbyIdAsync(product.Id)).ReturnsAsync(product);
            _productRepository.Setup(x => x.DeleteAsync(product));
            _uow.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);
            var service = CreateInstance();
            // Act
            var result = await service.DeleteAsync(product.Id);
            // Assert
            result.Should().BeTrue();
            _productRepository.Verify(x => x.DeleteAsync(product), Times.Once);
            _uow.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
