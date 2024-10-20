using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace WorkBuddy.MAUI.iOSHandlers
{
    public class CustomSearchBarHandler : SearchBarHandler
    {

        protected override void ConnectHandler(MauiSearchBar platformView)
        {
            base.ConnectHandler(platformView);
            // Remove the underline by setting the background to an empty image and border style to none
            platformView.BackgroundImage = new UIImage();
            platformView.SearchTextField.BorderStyle = UITextBorderStyle.None;
        }
    }
}
