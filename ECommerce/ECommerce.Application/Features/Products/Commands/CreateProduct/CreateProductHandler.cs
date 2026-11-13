using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Repositories;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECommerce.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductHandler(IUnitOfWork uow) : IRequestHandler<CreateProductCommand, CreateProductResponse>
    {
        public async Task<CreateProductResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = command.Adapt<Product>();
            var createdProduct = await uow.Products.CreateAsync(product);
            await uow.SaveChangesAsync();

            return createdProduct.Adapt<CreateProductResponse>();
        }
    }
}
