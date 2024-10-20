using Android.Content.Res;
using AndroidX.AppCompat.Widget;
using Microsoft.Maui.Handlers;

namespace WorkBuddy.MAUI.AndroidHandlers
{
    public class BorderlessEntryHandler : EntryHandler
    {
        protected override void ConnectHandler(AppCompatEditText platformView)
        {
            base.ConnectHandler(platformView);
            // Remove underline
            platformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
        }
    }
}
