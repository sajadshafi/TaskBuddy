using SQLite;

namespace WorkBuddy.MAUI.Infrastructure
{
    public class SqliteConnectionFactory
    {
        public ISQLiteAsyncConnection CreateConnection()
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "work-buddy.db3");
            return new SQLiteAsyncConnection(
                filePath, 
                SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
        }
    }
}
