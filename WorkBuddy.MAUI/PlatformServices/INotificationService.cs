namespace WorkBuddy.MAUI.PlatformServices;

public interface INotificationService
{
    void SendNotification(string title, string message, DateTime? notifyTime = null);
}