using System.Collections.ObjectModel;
using WorkBuddy.MAUI.Models;
using WorkBuddy.MAUI.PlatformServices;
using WorkBuddy.MAUI.Services;

namespace WorkBuddy.MAUI.ViewModels
{
    public class AddTaskViewModel : BaseViewModel
    {
        private readonly WorkItemService _workItemService;
        private readonly INotificationService _notificationService;
        public ObservableCollection<WorkspaceDto> Workspaces { get; set; } = [];
        public WorkspaceDto? SelectedWorkspace { get; set; }

        private readonly WorkspaceService _workspaceService;

        public Command CreateTaskCommand { get; private set; }

        public WorkItemDto WorkItemDto { get; set; }

        public AddTaskViewModel(WorkItemService workItemService, WorkspaceService workspaceService, INotificationService notificationService)
        {
            _workItemService = workItemService;
            _workspaceService = workspaceService;
            CreateTaskCommand = new(async () => await CreateWorkItemAsync());
            WorkItemDto = new WorkItemDto();
            GetWorkspaces();
            _notificationService = notificationService;
        }

        public void GetWorkspaces()
        {
            IEnumerable<WorkspaceDto> workspaces = _workspaceService
                .GetWorkspacesAsync().Result;

            Workspaces.Clear();
            foreach (WorkspaceDto workspace in workspaces)
            {
                Workspaces.Add(workspace);
            }
        }

        public async Task CreateWorkItemAsync()
        {
            WorkItemDto.WorkspaceId = SelectedWorkspace?.Id ?? 0;
            await _workItemService.CreateAsync(WorkItemDto);
            
            var time = WorkItemDto.ScheduledOnDate.Date + WorkItemDto.ScheduledAtTime;
            _notificationService.SendNotification("Reminder!", WorkItemDto.Title, time);

            await Shell.Current.GoToAsync("..");
        }
    }
}
