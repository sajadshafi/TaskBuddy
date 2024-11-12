using WorkBuddy.MAUI.Views.Pages;

namespace WorkBuddy.MAUI.Models
{
    public class Routes
    {
        public static readonly string MainPageRoute = nameof(MainPage);
        public static readonly string AddTaskPageRoute = nameof(AddTaskPage);
        public static readonly string TaskListPageRoute = nameof(TaskListPage);
        public static readonly string SettingPageRoute = nameof(SettingPage);
        public static readonly string WorkspacePageRoute = nameof(WorkspacesPage);
        public static readonly string BackRoute = "..";

    }
}
