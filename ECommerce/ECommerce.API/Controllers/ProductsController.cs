
using ECommerce.API.Extensions;
using ECommerce.Application.Features.Products.Commands.CreateProduct;
using ECommerce.Application.Features.Products.Queries.GetProducts;
using ECommerce.Application.Requests;
using ECommerce.Application.Services;
using ECommerce.Domain.Entities;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase //Bu sınıfın sorumluğu, gelen isteği alıp, değerlendirip işleyecek nesnelere paslamak ve sonucu istemciye döndürmektir.
    {

        private readonly IProductService _productService;
        private readonly IValidator<CreateProductCommand> _createValidator;
        private readonly IMediator _mediator;

        public ProductsController(IProductService productService, IValidator<CreateProductCommand> createValidator, IMediator mediator)
        {
            _productService = productService;
            _createValidator = createValidator;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var request = new GetProductsQuery();
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand request)
        {

            var validation = await _createValidator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return validation.ToValidationProblem(this);
            }

            //var product = new Product
            //{
            //    CategoryId = request.CategoryId,
            //    Name = request.Name,
            //    Price = request.Price,
            //    SKU = request.Sku,
            //    Stock = request.Stock,
            //};

            // var product = request.Adapt<Product>();

            //  await _productService.CreateAsync(product);

            var response = await _mediator.Send(request);

            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);


        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return product is null ? NotFound() : Ok(product);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            var existingProduct = await _productService.GetByIdAsync(id);

            //Dikkat!! Bu nesnenin bellekte olmasının tek sebebi buradaki önerme. Sizce bu maaliyet sayılmaz mı?
            if (existingProduct is null)
            {
                return NotFound(new { message = $"{id} id'li ürün db'de bulunamadı." });
            }

            await _productService.UpdateAsync(product);

            return NoContent();
        }

        [HttpPut("indirim/{id}")]
        public async Task<IActionResult> ChangePrice(int id, [FromQuery] decimal discount)
        {
            return Ok(await _productService.ApplyDiscount(id, discount));
        }


    }
}
