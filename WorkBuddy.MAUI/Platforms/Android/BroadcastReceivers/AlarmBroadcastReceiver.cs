using Android.Content;
using WorkBuddy.MAUI.Services;

namespace WorkBuddy.MAUI.BroadcastReceivers;

[BroadcastReceiver(Enabled = true, Label = "Local Notifications Broadcast Receiver")]
public class AlarmBroadcastReceiver : BroadcastReceiver
{
    public override void OnReceive(Context? context, Intent? intent)
    {
        if (intent?.Extras != null)
        {
            string? title = intent.GetStringExtra(NotificationService.TitleKey) ?? "";
            string? message = intent.GetStringExtra(NotificationService.MessageKey) ?? "";
            NotificationService manager = NotificationService.Instance ?? new NotificationService();
            manager.Show(title, message);
        }
    } 
}