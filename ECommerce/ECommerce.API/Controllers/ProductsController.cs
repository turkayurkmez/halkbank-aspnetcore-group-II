using ECommerce.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase //Bu sınıfın sorumluğu, gelen isteği alıp, değerlendirip işleyecek nesnelere paslamak ve sonucu istemciye döndürmektir.
    {

        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await Task.CompletedTask;
            return Ok(await _productService.GetAllProductsAsync());
        }
    }
}
