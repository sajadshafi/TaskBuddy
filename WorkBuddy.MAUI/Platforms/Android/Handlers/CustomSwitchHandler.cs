using AndroidX.AppCompat.Widget;
using Microsoft.Maui.Handlers;

namespace WorkBuddy.MAUI.AndroidHandlers
{
    public class CustomSwitchHandler : SwitchHandler
    {
        protected override void ConnectHandler(SwitchCompat platformView)
        {
            base.ConnectHandler(platformView);
            platformView.SetPadding(0,0,0,0);
        }
    }
}
