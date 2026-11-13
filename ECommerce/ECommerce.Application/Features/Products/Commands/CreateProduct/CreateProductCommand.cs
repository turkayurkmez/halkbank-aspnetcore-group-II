using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Products.Commands.CreateProduct
{
    //istek nasıl olacak?
    public record CreateProductCommand(string Name, string SKU, decimal Price, int Stock, int CategoryId) : IRequest<CreateProductResponse>;

    //yanıt ne dönecek?
    public record CreateProductResponse(int Id, string Name, string SKU, decimal Price, int Stock, int CategoryId);
}
