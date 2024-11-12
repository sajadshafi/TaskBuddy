using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using WorkBuddy.MAUI.Infrastructure;
using WorkBuddy.MAUI.PlatformServices;
using WorkBuddy.MAUI.Services;
using WorkBuddy.MAUI.Views.Pages;
using WorkBuddy.MAUI.ViewModels;

namespace WorkBuddy.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Montserrat-VariableFont_wght.ttf", "Monty");
                    fonts.AddFont("Inter-VariableFont_opsz.ttf", "Inter");

                    // Add material icon font
                    fonts.AddFont("MaterialIcons-Regular.ttf", "MIconsRegular");
                    fonts.AddFont("MaterialIconsOutlined-Regular.otf", "MIconsOutlined");
                    fonts.AddFont("MaterialIconsRound-Regular.otf", "MIconsRound");
                    fonts.AddFont("MaterialIconsSharp-Regular.otf", "MIconsSharp");
                    fonts.AddFont("MaterialIconsTwoTone-Regular.otf", "MIconsTwoTone");

                }).ConfigureMauiHandlers(handlers =>
                {
#if ANDROID
                    handlers.AddHandler<Controls.CustomSearchBar, AndroidHandlers.CustomSearchBarHandler>();
                    handlers.AddHandler<Controls.CustomPicker, AndroidHandlers.CustomPickerHandler>();
                    handlers.AddHandler<Controls.BorderlessEntry, AndroidHandlers.BorderlessEntryHandler>();
                    handlers.AddHandler<Controls.BorderlessEditor, AndroidHandlers.BorderlessEditorHandler>();
                    handlers.AddHandler<Controls.BorderlessDatePicker, AndroidHandlers.BorderlessDatePickerHandler>();
                    handlers.AddHandler<Controls.BorderlessTimePicker, AndroidHandlers.BorderlessTimePickerHandler>();
                    handlers.AddHandler<Controls.CustomSwitch, AndroidHandlers.CustomSwitchHandler>();
#endif

#if IOS
                    handlers.AddHandler<Controls.CustomSearchBar, iOSHandlers.CustomSearchBarHandler>();
                    handlers.AddHandler<Controls.CustomPicker, iOSHandlers.CustomPickerHandler>();
                    handlers.AddHandler<Controls.BorderlessEntry, iOSHandlers.BorderlessEntryHandler>();
                    handlers.AddHandler<Controls.BorderlessEditor, iOSHandlers.BorderlessEditorHandler>();
                    handlers.AddHandler<Controls.BorderlessDatePicker, iOSHandlers.BorderlessDatePickerHandler>();
                    handlers.AddHandler<Controls.BorderlessTimePicker, iOSHandlers.BorderlessTimePickerHandler>();
                    handlers.AddHandler<Controls.CustomSwitch, iOSHandlers.CustomSwitchHandler>();
#endif
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<SqliteConnectionFactory>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();

            builder.Services.AddTransient<AddTaskPage>();
            builder.Services.AddTransient<AddTaskViewModel>();

            builder.Services.AddTransient<TaskListPage>();
            builder.Services.AddTransient<TaskListViewModel>();

            builder.Services.AddTransient<WorkspacesPage>();
            builder.Services.AddTransient<WorkspaceViewModel>();

            builder.Services.AddTransient<SettingPage>();
            builder.Services.AddTransient<SettingViewModel>();

            builder.Services.AddSingleton<WorkItemService>();
            builder.Services.AddSingleton<WorkspaceService>();
            
            
            // Add platform based services
            #if ANDROID
            builder.Services.AddTransient<INotificationService, NotificationService>();
            #endif

            return builder.Build();
        }
    }
}
