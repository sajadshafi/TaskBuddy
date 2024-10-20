using SQLite;
using WorkBuddy.MAUI.Entities;
using WorkBuddy.MAUI.Infrastructure;
using WorkBuddy.MAUI.Models;

namespace WorkBuddy.MAUI.Services
{
    public class WorkItemService
    {
        private readonly SqliteConnectionFactory _sqliteConnectionFactory;
        private readonly ISQLiteAsyncConnection _connection;
        public WorkItemService(SqliteConnectionFactory sqliteConnectionFactory)
        {
            _sqliteConnectionFactory = sqliteConnectionFactory;
            _connection = _sqliteConnectionFactory.CreateConnection();
        }

        public async Task<IEnumerable<WorkItemDto>> GetWorkItemsAsync()
        {
            try
            {
                var items = await _connection.Table<WorkItem>()
                    .ToListAsync()
                    .ConfigureAwait(false);

                return items
                    .Select(item => 
                                WorkItemDto.Create(item))
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}:  {ex}");
                return [];
            }
        }

        public async Task<WorkItemDto> GetWorkItemByIdAsync(int id)
        {
            var item = await _connection.Table<WorkItem>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return WorkItemDto.Create(item);
        }

        public async Task<WorkItemDto> CreateAsync(WorkItemDto workItemDto)
        {
            WorkItem item = new()
            {
                Title = workItemDto.Title,
                Description = workItemDto.Description,
                IsCompleted = false,
                WorkspaceId = workItemDto.WorkspaceId,
                ScheduledOn = workItemDto.ScheduledOnDate.Add(workItemDto.ScheduledAtTime)
            };

            await _connection.InsertAsync(item);
            workItemDto.Id = item.Id;
            return workItemDto;
        }

        public async Task<WorkItemDto> UpdateAsync(WorkItemDto workItemDto)
        {
            WorkItem item = new()
            {
                Id = workItemDto.Id,
                Title = workItemDto.Title,
                Description = workItemDto.Description,
                IsCompleted = false,
                WorkspaceId = workItemDto.WorkspaceId,
                ScheduledOn = workItemDto.ScheduledOnDate.Add(workItemDto.ScheduledAtTime)
            };

            await _connection.UpdateAsync(item);
            return workItemDto;
        }

        public async Task<bool> CompleteAsync(int id)
        {
            WorkItem item = await _connection.Table<WorkItem>()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
            {
                return false;
            }

            item.IsCompleted = true;

            await _connection.UpdateAsync(item);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            WorkItem item = await _connection.Table<WorkItem>()
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
