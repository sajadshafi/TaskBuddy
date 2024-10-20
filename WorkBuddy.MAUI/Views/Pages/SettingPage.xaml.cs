using WorkBuddy.MAUI.ViewModels;

namespace WorkBuddy.MAUI.Views.Pages;

public partial class SettingPage : ContentPage
{
	public SettingPage(SettingViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}