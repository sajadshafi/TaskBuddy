using Android.Content.Res;
using Microsoft.Maui.Handlers;

namespace WorkBuddy.MAUI.AndroidHandlers
{
    public class CustomSearchBarHandler : SearchBarHandler
    {
        protected override void ConnectHandler(AndroidX.AppCompat.Widget.SearchView platformView)
        {
            base.ConnectHandler(platformView);

            // Remove underline from the native control
            var searchPlate = platformView.FindViewById<Android.Widget.LinearLayout>(Resource.Id.search_plate);
            searchPlate.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
        }
    }
}
