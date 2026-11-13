using Microsoft.Extensions.Logging;

namespace ECommerce.Application.Notifications
{
    public class SMSChannel : INotificationChannel
    {
        private readonly ILogger<SMSChannel> _logger;

        public SMSChannel(ILogger<SMSChannel> logger)
        {
            _logger = logger;
        }

        public string Name => "SMS";

        public Task SendAsync(string to, string message)
        {
            _logger.LogInformation($"[{Name}] {to} -> {message}");
            return Task.CompletedTask;
        }
    }




}

