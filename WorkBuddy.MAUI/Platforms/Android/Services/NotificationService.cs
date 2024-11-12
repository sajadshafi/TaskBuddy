using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using WorkBuddy.MAUI.BroadcastReceivers;
using WorkBuddy.MAUI.PlatformServices;

namespace WorkBuddy.MAUI.Services;

public class NotificationService : INotificationService
{
    
    #region Notification Channel Information
    private const string ChannelId = "default";
    private const string ChannelName = "Default";
    private const string ChannelDescription = "Notification channel for taskbuddy app";
    #endregion
    
    #region Notification keys
    public const string TitleKey = "title";
    public const string MessageKey = "message";
    #endregion
    
    #region private primitives
    private bool _channelInitialized = false;
    private int _messageId = 0;
    private int _pendingIntentId = 0;
#endregion
    
    private NotificationManagerCompat? _compatManager;
    public static NotificationService? Instance { get; private set; }
    
    public NotificationService()
    {
        if (Instance != null) return;
        CreateNotificationChannel();
        _compatManager = NotificationManagerCompat.From(Android.App.Application.Context);
        Instance = this;
    }
    
    public void SendNotification(string title, string message, DateTime? notifyTime = null)
    {
        if (!_channelInitialized)
        {
            CreateNotificationChannel();
        }
        
        if (notifyTime != null)
        {
            Intent intent = new Intent(Platform.AppContext, typeof(AlarmBroadcastReceiver));
            intent.PutExtra(TitleKey, title);
            intent.PutExtra(MessageKey, message);
            intent.SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTop);

            var pendingIntentFlags = (Build.VERSION.SdkInt >= BuildVersionCodes.S)
                ? PendingIntentFlags.CancelCurrent | PendingIntentFlags.Immutable
                : PendingIntentFlags.CancelCurrent;

            PendingIntent? pendingIntent = PendingIntent.GetBroadcast(Platform.AppContext, _pendingIntentId++, intent, pendingIntentFlags);
            if (Platform.AppContext.GetSystemService(Context.AlarmService) is AlarmManager alarmManager && pendingIntent != null)
            {
                long triggerTime = GetNotifyTime(notifyTime.Value);
                alarmManager.Set(AlarmType.RtcWakeup, triggerTime, pendingIntent);
            }
        }
        else
        {
            Show(title, message);
        }
    }
    
    public void Show(string title, string message)
    {
        Intent intent = new Intent(Platform.AppContext, typeof(MainActivity));
        intent.PutExtra(TitleKey, title);
        intent.PutExtra(MessageKey, message);
        intent.SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTop);

        var pendingIntentFlags = (Build.VERSION.SdkInt >= BuildVersionCodes.S)
            ? PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable
            : PendingIntentFlags.UpdateCurrent;

        PendingIntent? pendingIntent = PendingIntent.GetActivity(Platform.AppContext, _pendingIntentId++, intent, pendingIntentFlags);
        NotificationCompat.Builder builder = new NotificationCompat.Builder(Platform.AppContext, ChannelId)
            .SetContentIntent(pendingIntent)
            .SetContentTitle(title)
            .SetContentText(message)
            .SetSmallIcon(Microsoft.Maui.Resource.Drawable.notify_icon);
        // .SetLargeIcon(BitmapFactory.DecodeResource(Platform.AppContext.Resources, Resource.Drawable.dotnet_logo))

        Notification notification = builder.Build();
        if(_compatManager != null) _compatManager.Notify(_messageId++, notification);  
    }
    
    void CreateNotificationChannel()
    {
        if(Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            var channelNameJava = new Java.Lang.String(ChannelName);
            NotificationChannel channel = new NotificationChannel(ChannelId, channelNameJava, NotificationImportance.Default)
            {
                Description = ChannelDescription
            };
            
            NotificationManager? notificationManager = (NotificationManager?)Platform.AppContext.GetSystemService(Context.NotificationService);

            if (notificationManager != null)
            {
                notificationManager.CreateNotificationChannel(channel);
                _channelInitialized = true;
            }
        }
    }
    
    long GetNotifyTime(DateTime notifyTime)
    {
        DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(notifyTime);
        double epochDiff = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;
        long utcAlarmTime = utcTime.AddSeconds(-epochDiff).Ticks / 10000;
        return utcAlarmTime; // milliseconds
    }
}