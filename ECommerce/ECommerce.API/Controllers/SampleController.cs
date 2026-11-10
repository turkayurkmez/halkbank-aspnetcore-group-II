using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public SampleController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Get()
        {
            return Ok(new { status = "Burası; Sample/Index enpointi" });
        }

        [HttpGet("boom")]
        public IActionResult Boom()
        {
            if (!_env.IsDevelopment())
            {
                return NotFound();
            }
            throw new InvalidOperationException("Bilerek deney amaçlı hata fırlatıyoruz!");
        }
    }
}
