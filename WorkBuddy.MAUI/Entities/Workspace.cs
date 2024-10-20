using SQLite;

namespace WorkBuddy.MAUI.Entities
{
    [Table("workspaces")]
    public class Workspace : BaseModel
    {
        [MaxLength(100), Unique]
        public string Name { get; set; } = string.Empty;
    }
}
