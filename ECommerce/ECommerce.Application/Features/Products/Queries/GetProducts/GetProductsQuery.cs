using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Products.Queries.GetProducts
{
    public record GetProductsQuery : IRequest<IEnumerable<GetProductResponse>>;

    public record GetProductResponse(int Id, string Name, string SKU, decimal Price, int Stock, int CategoryId);


   

}
