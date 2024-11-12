using WorkBuddy.MAUI.Models;
using WorkBuddy.MAUI.Views.Pages;

namespace WorkBuddy.MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Configuring routes
            Routing.RegisterRoute(Routes.MainPageRoute, typeof(MainPage));
            Routing.RegisterRoute(Routes.AddTaskPageRoute, typeof(AddTaskPage));
            Routing.RegisterRoute(Routes.TaskListPageRoute, typeof(TaskListPage));
            Routing.RegisterRoute(Routes.SettingPageRoute, typeof(SettingPage));
            Routing.RegisterRoute(Routes.WorkspacePageRoute, typeof(WorkspacesPage));
        }
    }
}
