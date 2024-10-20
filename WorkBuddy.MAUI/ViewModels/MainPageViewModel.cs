using System.Collections.ObjectModel;
using WorkBuddy.MAUI.Models;
using WorkBuddy.MAUI.Services;

namespace WorkBuddy.MAUI.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly WorkItemService _workItemService;
        public ObservableCollection<WorkItemDto> WorkItems { get; private set; } = [];

        public MainPageViewModel(WorkItemService workItemService)
        {
            _workItemService = workItemService;
        }

        public async Task PopulateTasksAsync()
        {
            IEnumerable<WorkItemDto> workItems = await _workItemService.GetWorkItemsAsync();

            if (workItems.Any()) {
                WorkItems.Clear();
            }
            foreach (WorkItemDto workItem in workItems)
            {
                WorkItems.Add(workItem);
            }
        }
    }
}
