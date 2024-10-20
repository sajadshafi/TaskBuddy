using SQLite;

namespace WorkBuddy.MAUI.Entities
{
    public class WorkItem : BaseModel
    {
        [MaxLength(100), Unique]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime ScheduledOn { get; set; }
        public bool IsCompleted { get; set; }
        public int WorkspaceId { get; set; }
    }
}
