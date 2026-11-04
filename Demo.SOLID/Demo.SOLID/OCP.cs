using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.SOLID
{
    public enum NotificationTypes
    {
        SMS,
        EMail
    }

    public class NotificationServiceBefore
    {
        public void Notify(NotificationTypes type, string message)
        {
            switch (type)
            {
                case NotificationTypes.SMS:
                    Console.WriteLine("SMS gönderildi");
                    break;
                case NotificationTypes.EMail:
                    Console.WriteLine("Mail gönderildi");
                    break;
                default:
                    break;
            }
        }
    }

    public interface INotification
    {
        void Send(string message);
    }

    public class EmailNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine("mesaj eposta ile gönderildi");
        }
    }

    public class SMSNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine("mesaj SMS ile gönderildi");
        }
    }

    public class TelegramNotifier : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine("Telegram.....");
        }
    }

    public class NotificationServicAfter
    {
        public void Notify(INotification type, string message)
        {
            type.Send(message);
        }
    }
}
