using SQLite;
using WorkBuddy.MAUI.Entities;
using WorkBuddy.MAUI.Infrastructure;
using WorkBuddy.MAUI.Models;

namespace WorkBuddy.MAUI.Services
{
    public class WorkspaceService
    {
        SqliteConnectionFactory _sqliteConnectionFactory;
        private readonly ISQLiteAsyncConnection _connection;
        public WorkspaceService(SqliteConnectionFactory sqliteConnectionFactory)
        {
            _sqliteConnectionFactory = sqliteConnectionFactory;
            _connection = sqliteConnectionFactory.CreateConnection();
        }

        public async Task<IEnumerable<WorkspaceDto>> GetWorkspacesAsync()
        {
            try
            {
                var items = await _connection.Table<Workspace>()
                    .ToListAsync()
                    .ConfigureAwait(false);

                return items
                    .Select(item =>
                               new WorkspaceDto(item.Id, item.Name))
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}:  {ex}");
                return [];
            }
        }

        public async Task<WorkspaceDto> CreateAsync(WorkspaceDto workspaceDto)
        {
            Workspace item = new()
            {
                Name = workspaceDto.Name,
            };

            await _connection.InsertAsync(item);
            workspaceDto = new(item.Id, item.Name);
            return workspaceDto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Workspace item = await _connection.Table<Workspace>()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
            {
                return false;
            }

            await _connection.DeleteAsync(item);
            return true;
        }
    }
}
