using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace WorkBuddy.MAUI.iOSHandlers
{
    public class BorderlessEditorHandler : EditorHandler
    {
        protected override void ConnectHandler(MauiTextView platformView)
        {
            base.ConnectHandler(platformView);
            platformView.BackgroundColor = UIColor.FromRGBA(0,0,0,0);
            platformView.BorderStyle = UITextViewBorderStyle.None;
        }
    }
}
