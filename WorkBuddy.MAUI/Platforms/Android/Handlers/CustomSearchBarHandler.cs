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
            var context = platformView.Context;
            var searchPlate = platformView.FindViewById<Android.Widget.LinearLayout>(Resource.Id.search_plate);
            var searchIconId = context?.Resources?.GetIdentifier("search_mag_icon", "id", context.PackageName);

            if (searchIconId is not null && searchIconId != 0)
            {
                var searchIcon = platformView.FindViewById<Android.Widget.ImageView>(searchIconId.Value);
                if (searchIcon != null) {
                    searchIcon.SetColorFilter(Android.Graphics.Color.Rgb(255,255,255), Android.Graphics.PorterDuff.Mode.Multiply);
                }
            }

            if (searchPlate is not null)
            {
                searchPlate.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
                searchPlate.SetBackgroundColor(Android.Graphics.Color.White);
            }
        }
    }
}
