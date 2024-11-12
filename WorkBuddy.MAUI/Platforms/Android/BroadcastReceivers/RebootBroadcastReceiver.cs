using Android.App;
using Android.Content;
using WorkBuddy.MAUI.PlatformServices;
using WorkBuddy.MAUI.Services;

namespace WorkBuddy.MAUI.BroadcastReceivers;

[BroadcastReceiver(Enabled = true, Exported = true)]
[IntentFilter([Intent.ActionBootCompleted])]
public class RebootBroadcastReceiver : BroadcastReceiver
{
    private readonly INotificationService? _notificationService;
    private readonly WorkItemService? _workItemService;

    public RebootBroadcastReceiver()
    {
        _notificationService = IPlatformApplication.Current?.Services.GetService<INotificationService>();
        _workItemService = IPlatformApplication.Current?.Services.GetService<WorkItemService>();
    }

    // public RebootBroadcastReceiver(INotificationService? notificationService, WorkItemService? workItemService)
    // {
    //     _notificationService = notificationService;
    //     _workItemService = workItemService;
    // }

    public override async void OnReceive(Context? context, Intent? intent)
    {
        
        if (intent is not null && intent.Action == Intent.ActionBootCompleted)
        {
            var workItems = await _workItemService.GetUpcomingWorkItemsAsync();

            foreach (var workItem in workItems)
            {
                _notificationService.SendNotification("Reminder!", workItem.Title, workItem.ScheduledOnDate);
            }
        }
    }
}