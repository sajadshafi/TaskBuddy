using WorkBuddy.MAUI.PlatformServices;
using WorkBuddy.MAUI.ViewModels;

namespace WorkBuddy.MAUI.Views.Pages;

public partial class WorkspacesPage : ContentPage
{
    public WorkspacesPage(WorkspaceViewModel viewmodel)
    {
        InitializeComponent();
        BindingContext = viewmodel;
    }
}