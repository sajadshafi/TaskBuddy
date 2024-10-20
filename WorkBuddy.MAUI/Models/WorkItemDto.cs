using WorkBuddy.MAUI.Entities;

namespace WorkBuddy.MAUI.Models
{
    public class WorkItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime ScheduledOnDate { get; set; } = DateTime.Now;
        public TimeSpan ScheduledAtTime { get; set; } = (DateTime.Now).TimeOfDay;
        public bool IsCompleted { get; set; }
        public int WorkspaceId { get; set; }
        public string WorkspaceName { get; set; } = string.Empty;
        internal static WorkItemDto Create(WorkItem item)
        {
            return new()
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                ScheduledOnDate = item.ScheduledOn,
                WorkspaceId = item.WorkspaceId,
                ScheduledAtTime = item.ScheduledOn.TimeOfDay,
                IsCompleted = item.IsCompleted,
            };
        }
    }
}
