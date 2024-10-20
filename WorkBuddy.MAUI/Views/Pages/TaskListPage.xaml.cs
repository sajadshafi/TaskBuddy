using WorkBuddy.MAUI.ViewModels;

namespace WorkBuddy.MAUI.Views.Pages;

public partial class TaskListPage : ContentPage
{
	public TaskListPage(TaskListViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}