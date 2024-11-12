using System.Collections.ObjectModel;
using WorkBuddy.MAUI.Models;
using WorkBuddy.MAUI.Services;

namespace WorkBuddy.MAUI.ViewModels
{
    public class TaskListViewModel : BaseViewModel
    {
        public ObservableCollection<WorkItemDto> WorkItems { get; set; } = [];
        public ObservableCollection<WorkspaceDto> Workspaces { get; set; } = [];
        private WorkspaceDto? _selectedWorkspace;
        private bool _isRefreshing = false;
        private bool _isWorkspaceDropdownVisible = false;

        #region Commands
        public Command DeleteTaskCommand { get; private set; }
        public Command GetTasksCommand { get; private set; }
        public Command SelectWorkspaceCommand { get; private set; }
        public Command CreateWorkspaceCommand { get; private set; }
        public Command ShowWorkspaceCommand { get; private set; }
        #endregion

        private readonly WorkItemService _workItemService;
        private readonly WorkspaceService _workspaceService;

        public TaskListViewModel(WorkItemService workItemService, WorkspaceService workspaceService)
        {
            _workItemService = workItemService;
            _workspaceService = workspaceService;
            GetTasksCommand = new(GetTasks);
            DeleteTaskCommand = new Command<int>(async (itemId) => await DeleteTaskAsync(itemId));
            SelectWorkspaceCommand = new Command<WorkspaceDto>(async (workspace) => await SelectWorkspaceAsync(workspace));
            ShowWorkspaceCommand = new(ShowWorkspaceDropdown);
            CreateWorkspaceCommand = new Command(async () => await CreateWorkspaceAsync());
            GetWorkspaces();
            GetTasks();
        }

        public WorkspaceDto SelectedWorkspace
        {
            get => _selectedWorkspace;
            set
            {
                _selectedWorkspace = value;
                OnPropertyChanged(); // Make sure to implement INotifyPropertyChanged
            }
        }

        public void GetTasks()
        {
            IsRefreshing = true;
            IEnumerable<WorkItemDto> workItems = _workItemService
                .GetWorkItemsAsync().Result;

            if (workItems.Any())
            {
                WorkItems.Clear();
            }
            foreach (WorkItemDto workItem in workItems)
            {
                WorkItems.Add(workItem);
            }

            IsRefreshing = false;
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

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                if (_isRefreshing == value)
                    return;
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }

        public async Task SelectWorkspaceAsync(WorkspaceDto workspace)
        {
            if (workspace == null) return;

            if (workspace.Id == 0)
            {
                var workspaceName = await Shell.Current.DisplayPromptAsync("Workspace", "Create new workspace", "Ok", "Cancel");

                if (workspaceName == null) return;

                var response = await _workspaceService.CreateAsync(new(0,workspaceName));

                Workspaces.Add(response);
            }

            return;
        }

        public async Task DeleteTaskAsync(int id)
        {
            // Display a confirmation alert
            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert(
                "Confirm Delete",
                "Are you sure you want to delete this task?",
                "Yes",
                "No");

            if (isConfirmed)
            {
                await _workItemService.DeleteAsync(id);
                var item = WorkItems.FirstOrDefault(x => x.Id == id);
                if (item is not null)
                {
                    WorkItems.Remove(item);
                }
            }
        }

        public bool IsWorkspaceDropdownVisible
        {
            get => _isWorkspaceDropdownVisible;
            set
            {
                if (_isWorkspaceDropdownVisible == value)
                    return;
                _isWorkspaceDropdownVisible = value;
                OnPropertyChanged();
            }
        }

        public void ShowWorkspaceDropdown()
        {
            IsWorkspaceDropdownVisible = !IsWorkspaceDropdownVisible;
        }

        public async Task CreateWorkspaceAsync()
        {

            var workspaceName = await Shell.Current.DisplayPromptAsync("Workspace", "Create new workspace", "Ok", "Cancel");

            if (workspaceName == null) return;

            var response = await _workspaceService.CreateAsync(new(0, workspaceName));

            Workspaces.Add(response);

            return;
        }

    }
}
