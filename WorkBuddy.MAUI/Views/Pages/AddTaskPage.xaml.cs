using WorkBuddy.MAUI.ViewModels;

namespace WorkBuddy.MAUI.Views.Pages;

public partial class AddTaskPage : ContentPage
{
	public AddTaskPage(AddTaskViewModel viewmodel)
	{
        InitializeComponent();
		BindingContext = viewmodel;
	}
}