
using ECommerce.API.Models.Requests;
using ECommerce.API.Services;
using ECommerce.API.Validators;
using ECommerce.Infrastructure.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase //Bu sınıfın sorumluğu, gelen isteği alıp, değerlendirip işleyecek nesnelere paslamak ve sonucu istemciye döndürmektir.
    {

        private readonly IProductService _productService;
        private readonly IValidator<CreateProductRequest> _createValidator;

        public ProductsController(IProductService productService, IValidator<CreateProductRequest> createValidator)
        {
            _productService = productService;
            _createValidator = createValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await Task.CompletedTask;
            return Ok(await _productService.GetProductSummaries());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {

            var validation = await _createValidator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                foreach (var item in validation.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    return ValidationProblem(ModelState);
                }
            }

            var product = new Product
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
                Price = request.Price,
                SKU = request.Sku,
                Stock = request.Stock,
            };

            await _productService.CreateAsync(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
          

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
