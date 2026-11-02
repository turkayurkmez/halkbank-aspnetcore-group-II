using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok(new { status = "Burası; Sample/Index enpointi" });
        }
    }
}
