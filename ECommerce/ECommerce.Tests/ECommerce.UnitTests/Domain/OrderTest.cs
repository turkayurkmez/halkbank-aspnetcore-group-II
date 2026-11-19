using ECommerce.Domain.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UnitTests.Domain
{
    public class OrderTest
    {
        [Fact]
        public void CalculateTotal_When_QuantityAtLeastTen_Applies_5PercentDiscount()
        {
            // Arrange
            var order = new Order()
            {
                Items = [new OrderItem { ProductId = 1, Quantity = 10, UnitPrice = 100 }]
            };

            // Act
            var total = order.CalculateTotal();
            // Assert

            total.Should().Be(950); // 1000 - 5% discount

        }

        [Fact]
        public void CalculateTotal_When_QuantityLessThanTen_Returns_Zero()
        {
            // Arrange
            var order = new Order()
            {
                Items = [new OrderItem { ProductId = 1, Quantity = 5, UnitPrice = 100 }]
            };
            // Act
            var total = order.CalculateTotal();
            // Assert
            total.Should().Be(500); // No discount applied
        }

        [Fact]
        public void CalculateTotal_WhenQuantity_SpreadsAcrossMultipleItems_Applies_5PercentDiscount()
        {
            // Arrange
            var order = new Order()
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductId = 1, Quantity = 5, UnitPrice = 100 },
                    new OrderItem { ProductId = 2, Quantity = 6, UnitPrice = 50 }
                }
            };
            // Act
            var total = order.CalculateTotal();
            // Assert
            total.Should().Be(760); // Total is 800 - 5% discount
        }
    }
}
