using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace WorkBuddy.MAUI.iOSHandlers
{
    public class BorderlessEntryHandler : EntryHandler
    {
        protected override void ConnectHandler(MauiTextField platformView)
        {
            base.ConnectHandler(platformView);
            platformView.Background = new UIImage();
            platformView.BorderStyle = UITextBorderStyle.None;
        }
    }
}
