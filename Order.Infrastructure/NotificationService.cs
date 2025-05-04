using Microsoft.Extensions.Logging;

namespace Order.Infrastructure;

public abstract class NotificationService(ILogger<NotificationService> logger) : INotificationService
{
    public abstract void SendNotification(string message);

    public void Log(string message)
    {
        logger.LogInformation("Notification logged {Message}", message);
    }
}

public interface INotificationService
{
    void SendNotification(string message);
}

public class SMSNotifiactionService(ILogger<NotificationService> logger) : NotificationService(logger)
{
    public override void SendNotification(string message)
    {
        throw new NotImplementedException();
    }
}

public class EmailNotificationService(ILogger<NotificationService> logger) : NotificationService(logger)
{
    public override void SendNotification(string message)
    {
        Console.WriteLine($"Email sent {message}");
    }
}