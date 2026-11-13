

using ECommerce.Application.Services;
using ECommerce.Domain.Entities;

using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            var created = await orderService.CreateAsync(order);
            return Created("https://sampleUrl.com", value: created);
        }
    }
}
