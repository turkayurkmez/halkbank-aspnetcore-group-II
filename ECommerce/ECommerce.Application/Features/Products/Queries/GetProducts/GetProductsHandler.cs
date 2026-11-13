using ECommerce.Infrastructure.Repositories;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Products.Queries.GetProducts
{
    public class GetProductsHandler(IUnitOfWork uow) : IRequestHandler<GetProductsQuery, IEnumerable<GetProductResponse>>
    {
        public async Task<IEnumerable<GetProductResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await uow.Products.GetAllAsync();
            return products.Adapt<IEnumerable<GetProductResponse>>();
        }
    }
}
