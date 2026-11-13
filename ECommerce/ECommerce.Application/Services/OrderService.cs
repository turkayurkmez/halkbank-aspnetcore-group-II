


using ECommerce.Application.Notifications;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services
{

    public class OrderService : IOrderService
    {
        private readonly NotificationService notificationService;

        public OrderService(NotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await notificationService.NotifyAsync(order.CustomerId.ToString(), "Siparişiniz alındı");
            return order;
        }
    }
}
