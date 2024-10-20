using Android.Content.Res;
using AndroidX.AppCompat.Widget;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Handlers;

namespace WorkBuddy.MAUI.AndroidHandlers
{
    public class BorderlessEditorHandler : EditorHandler
    {

        protected override void ConnectHandler(AppCompatEditText platformView)
        {
            base.ConnectHandler(platformView);
            platformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            platformView.TextCursorDrawable.SetTint(Colors.White.ToAndroid());
        }
    }
}
