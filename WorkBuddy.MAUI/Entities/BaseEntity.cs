using SQLite;

namespace WorkBuddy.MAUI.Entities
{
    public class BaseModel
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
