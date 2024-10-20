using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace WorkBuddy.MAUI.iOSHandlers
{
    public class BorderlessTimePickerHandler : TimePickerHandler
    {
        protected override void ConnectHandler(MauiTimePicker platformView)
        {
            base.ConnectHandler(platformView);
            platformView.Background = new UIImage();
            platformView.BackgroundColor = UIColor.Clear;
            platformView.BorderStyle = UITextBorderStyle.None;
        }
    }
}
