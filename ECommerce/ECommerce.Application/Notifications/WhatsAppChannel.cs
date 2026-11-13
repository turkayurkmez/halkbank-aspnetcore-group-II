using Microsoft.Extensions.Logging;

namespace ECommerce.Application.Notifications
{
    public class WhatsAppChannel : INotificationChannel
    {
        private ILogger<WhatsAppChannel> _logger;

        public WhatsAppChannel(ILogger<WhatsAppChannel> logger)
        {
            _logger = logger;
        }

        public string Name => "WhatsApp";

        public Task SendAsync(string to, string message)
        {
            _logger.LogInformation($"[WhatsApp] {to} -> {message}");
            return Task.CompletedTask;
        }
    }
}
