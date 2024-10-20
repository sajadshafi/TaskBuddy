using Android.Content.Res;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace WorkBuddy.MAUI.AndroidHandlers
{
    public class CustomPickerHandler : PickerHandler
    {
        protected override void ConnectHandler(MauiPicker platformView)
        {
            base.ConnectHandler(platformView);

            // Set the background to transparent
            platformView.BackgroundTintList = ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
            platformView.SetPadding(0,0,0,0);
            platformView.TextCursorDrawable.SetTint(Colors.White.ToAndroid());
        }
    }
}
