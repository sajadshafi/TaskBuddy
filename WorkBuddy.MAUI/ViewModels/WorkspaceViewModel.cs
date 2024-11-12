using System.Collections.ObjectModel;
using WorkBuddy.MAUI.Models;
using WorkBuddy.MAUI.Services;

namespace WorkBuddy.MAUI.ViewModels
{
    public class WorkspaceViewModel : BaseViewModel
    {
        public ObservableCollection<WorkspaceDto> Workspaces { get; set; } = [];

        #region Commands
        public Command DeleteWorkspaceCommand { get; private set; }
        public Command GetWorkspacesCommand { get; private set; }
        public Command CreateWorkspaceCommand { get; private set; }
        #endregion

        private readonly WorkspaceService _workspaceService;
        private bool _isRefreshing = false;

        public WorkspaceViewModel(WorkspaceService workspaceService)
        {
            DeleteWorkspaceCommand = new Command<int>(async (itemId) => await DeleteTaskAsync(itemId));
            GetWorkspacesCommand = new Command(() => GetWorkspaces());
            CreateWorkspaceCommand = new Command(async () => await CreateWorkspaceAsync());
            _workspaceService = workspaceService;
            GetWorkspaces();
        }

        public void GetTasks()
        {
            IsRefreshing = true;
            IEnumerable<WorkspaceDto> workspaces = _workspaceService
                .GetWorkspacesAsync().Result;

            if (workspaces.Any())
            {
                Workspaces.Clear();
            }
            foreach (WorkspaceDto workspace in workspaces)
            {
                Workspaces.Add(workspace);
            }

            IsRefreshing = false;
        }

        public void GetWorkspaces()
        {
            IsRefreshing = true;
            IEnumerable<WorkspaceDto> workspaces = _workspaceService
                .GetWorkspacesAsync().Result;

            Workspaces.Clear();
            foreach (WorkspaceDto workspace in workspaces)
            {
                Workspaces.Add(workspace);
            }
            IsRefreshing = false;
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
                await _workspaceService.DeleteAsync(id);
                var item = Workspaces.FirstOrDefault(x => x.Id == id);
                if (item is not null)
                {
                    Workspaces.Remove(item);
                }
            }
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
