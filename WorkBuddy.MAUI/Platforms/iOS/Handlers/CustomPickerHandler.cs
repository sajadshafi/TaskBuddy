using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace WorkBuddy.MAUI.iOSHandlers
{
    public class CustomPickerHandler : PickerHandler
    {
        protected override void ConnectHandler(MauiPicker platformView)
        {
            base.ConnectHandler(platformView);

            // Set the background to clear
            platformView.BackgroundColor = UIColor.Clear; // Remove the background color
        }
    }
}
