using Android.Content.Res;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace WorkBuddy.MAUI.AndroidHandlers
{
    public class BorderlessDatePickerHandler : DatePickerHandler
    {
        protected override void ConnectHandler(MauiDatePicker platformView)
        {
            base.ConnectHandler(platformView);
            platformView.BackgroundTintList = ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
            platformView.TextCursorDrawable.SetTint(Colors.White.ToAndroid());
        }
    }
}
