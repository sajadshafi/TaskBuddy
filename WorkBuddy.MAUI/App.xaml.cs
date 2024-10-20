using SQLite;
using WorkBuddy.MAUI.Entities;
using WorkBuddy.MAUI.Infrastructure;

namespace WorkBuddy.MAUI
{
    public partial class App : Application
    {
        private readonly SqliteConnectionFactory _sqliteConnectionFactory;
        public App(SqliteConnectionFactory sqliteConnectionFactory)
        {
            InitializeComponent();
            UserAppTheme = AppTheme.Dark;
            MainPage = new AppShell();
            _sqliteConnectionFactory = sqliteConnectionFactory;

        }

        protected override async void OnStart()
        {
            try
            {
                ISQLiteAsyncConnection context = _sqliteConnectionFactory.CreateConnection();

                // create tables
                await context.CreateTableAsync<Workspace>();
                await context.CreateTableAsync<WorkItem>();
            }
            catch (SQLiteException ex)
            {
                if (MainPage is not null)
                {
                    await MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
            }
            finally
            {
                base.OnStart();
            }
        }
    }
}
