using System.ComponentModel;
using WorkBuddy.MAUI.ViewModels;

namespace WorkBuddy.MAUI.Views.Pages;

public partial class TaskListPage : ContentPage
{
	public TaskListPage(TaskListViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
        ((TaskListViewModel)BindingContext).PropertyChanged += OnViewModelPropertyChanged;
    }

    private async void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TaskListViewModel.IsWorkspaceDropdownVisible))
        {
            if (sender is null) return;

            var viewModel = (TaskListViewModel)sender;

            if (viewModel is null) return;

            if (viewModel.IsWorkspaceDropdownVisible)
            {
                workspace_dropdown.IsVisible = true;
                await workspace_dropdown_arrow.RotateTo(270, 150);
                await workspace_dropdown.ScaleYTo(1, 150);
            }
            else
            {
                await workspace_dropdown_arrow.RotateTo(90, 150);
                await workspace_dropdown.ScaleYTo(0, 150);
                workspace_dropdown.IsVisible = false;
            }
        }
    }

    // TaskListPage.xaml.cs
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Assuming your TaskListViewModel has a method or command to refresh the tasks
        var viewModel = BindingContext as TaskListViewModel;
        viewModel?.GetTasksCommand.Execute(null);
    }
}