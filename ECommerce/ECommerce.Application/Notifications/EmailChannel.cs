using Microsoft.Extensions.Logging;

namespace ECommerce.Application.Notifications
{
    public class EmailChannel : INotificationChannel
    {
        private readonly ILogger<EmailChannel> _logger;

        public EmailChannel(ILogger<EmailChannel> logger)
        {
            _logger = logger;
        }

        public string Name => "E-Posta";

        public Task SendAsync(string to, string message)
        {

            _logger.LogInformation($"[{Name}] {to} -> {message}");
            return Task.CompletedTask;
        }
    }




}

