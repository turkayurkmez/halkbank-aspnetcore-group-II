


using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services
{
    public interface IOrderService
    {
        Task<Order> CreateAsync(Order order);
    }
}