using ECommerce.API.Models;
using ECommerce.API.Notifications;

namespace ECommerce.API.Services
{

    public class OrderService
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
