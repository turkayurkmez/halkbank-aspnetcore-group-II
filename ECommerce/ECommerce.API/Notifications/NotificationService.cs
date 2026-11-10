namespace ECommerce.API.Notifications
{
    public class NotificationService
    {
        private readonly IEnumerable<INotificationChannel> _channels;

        public NotificationService(IEnumerable<INotificationChannel> channels)
        {
            _channels = channels;
        }

        public async Task NotifyAsync(string to, string message)
        {
            foreach (var channel in _channels)
            {
                await channel.SendAsync(to, message);
            }
        }

    }


}

