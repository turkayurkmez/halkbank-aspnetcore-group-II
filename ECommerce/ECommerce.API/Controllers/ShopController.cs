
using ECommerce.Application.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IOptions<ShopOptions> _options;
        private readonly IWebHostEnvironment _environment;

        private readonly IConfiguration _configuration;

        public ShopController(IOptions<ShopOptions> options, IWebHostEnvironment environment, IConfiguration configuration)
        {
            _options = options;
            _environment = environment;
            _configuration = configuration;
        }

        [HttpGet("info")]
        public IActionResult Info()
        {
            var shopOptions = _options.Value;

            //asla gizli bir key'i response ile göndermeyin, onun yerine varlığını kontrol edin:
            var isFindUserSecret = !string.IsNullOrEmpty(_configuration["ExternalServices:PaymentApiKey"]);


            return Ok(new
            {
                storeName = shopOptions.StoreName,
                currency = shopOptions.Currency,
                maxOrderItems = shopOptions.MaxOrderItems,
                environment = _environment.EnvironmentName,
                paymentConfigured = isFindUserSecret,
                // paymentApi = _configuration["ExternalServices:PaymentApiKey"]
            });
        }
    }
}
