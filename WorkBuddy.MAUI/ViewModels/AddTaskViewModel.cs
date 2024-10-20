using WorkBuddy.MAUI.Models;
using WorkBuddy.MAUI.Services;

namespace WorkBuddy.MAUI.ViewModels
{
    public class AddTaskViewModel : BaseViewModel
    {
        private readonly WorkItemService _workItemService;

        public WorkItemDto WorkItemDto { get; set; }

        public Command CreateTaskCommand { get; private set; }
        public AddTaskViewModel(WorkItemService workItemService)
        {
            _workItemService = workItemService;
            WorkItemDto = new WorkItemDto();
            CreateTaskCommand = new(async () => await CreateWorkItemAsync());
        }

        public async Task CreateWorkItemAsync()
        {
            await _workItemService.CreateAsync(WorkItemDto);
            await Shell.Current.GoToAsync("..");
        }

    }
}
