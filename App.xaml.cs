using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.EntityFrameworkCore;

using SoftMarineWPF_MVVM.Contracts.Services;
using SoftMarineWPF_MVVM.Contracts.Views;
using SoftMarineWPF_MVVM.Core.Context;
using SoftMarineWPF_MVVM.Core.Contracts.Services;
using SoftMarineWPF_MVVM.Core.Services;
using SoftMarineWPF_MVVM.Services;
using SoftMarineWPF_MVVM.ViewModels;
using SoftMarineWPF_MVVM.Views;

namespace SoftMarineWPF_MVVM;

// For more information about application lifecycle events see https://docs.microsoft.com/dotnet/framework/wpf/app-development/application-management-overview

// WPF UI elements use language en-US by default.
// If you need to support other cultures make sure you add converters and review dates and numbers in your UI to ensure everything adapts correctly.
// Tracking issue for improving this is https://github.com/dotnet/wpf/issues/1946
public partial class App : Application
{
    private IHost _host;

    public T GetService<T>()
        where T : class
        => _host.Services.GetService(typeof(T)) as T;

    public App()
    {
    }

    private async void OnStartup(object sender, StartupEventArgs e)
    {
        var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        // For more information about .NET generic host see  https://docs.microsoft.com/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.0
        _host = Host.CreateDefaultBuilder(e.Args)
                .ConfigureAppConfiguration(c =>
                {
                    c.SetBasePath(appLocation);
                })
                .ConfigureServices(ConfigureServices)
                .Build();

        await _host.StartAsync();
    }

    private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {

        services.AddHostedService<ApplicationHostService>();

        // SQLite имеет какой-то баг в провайдере, поэтому не вариант использовать, а у меня времени на разбирательство нет сейчас.
        //
        //var sqliteConnectionString = @"DataSource=Data.db;";
        //services.AddDbContext<CoreContext>(x => x.UseSqlite(sqliteConnectionString));

        var npgsqlConnectionString = $"Server=127.0.0.1;Port=5432;Database=Tester;User ID=ProductionDatabase;Password=eQQ1p9u7TehL;Pooling=True;Command Timeout=300";

        services.AddDbContext<CoreContext>(s =>
        {
            s.UseNpgsql(npgsqlConnectionString);
        });

        // Services
        services.AddScoped<IDbService, DbService>();
        services.AddScoped<IInspectionService, InspectionService>();
        services.AddScoped<IInspectorService, InspectorService>();
        services.AddScoped<IRemarkService, RemarkService>();

        services.AddSingleton<IWindowManagerService, WindowManagerService>();
        services.AddSingleton<IPageService, PageService>();
        services.AddSingleton<INavigationService, NavigationService>();

        // Views and ViewModels
        services.AddTransient<IShellWindow, ShellWindow>();
        services.AddTransient<ShellViewModel>();

        services.AddTransient<InspectionViewModel>();
        services.AddTransient<InspectionPage>();

        services.AddTransient<InspectorViewModel>();
        services.AddTransient<InspectorPage>();

        services.AddTransient<InspectorDialogCreateViewModel>();
        services.AddTransient<InspectorDialogCreatePage>();

        services.AddTransient<InspectorDialogUpdateViewModel>();
        services.AddTransient<InspectorDialogUpdatePage>();

        services.AddTransient<InspectionDialogCreateViewModel>();
        services.AddTransient<InspectionDialogCreatePage>();

        services.AddTransient<InspectionDialogUpdateViewModel>();
        services.AddTransient<InspectionDialogUpdatePage>();

        services.AddTransient<RemarkDialogCreateViewModel>();
        services.AddTransient<RemarkDialogCreatePage>();

        services.AddTransient<RemarkDialogUpdateViewModel>();
        services.AddTransient<RemarkDialogUpdatePage>();

        services.AddTransient<IShellDialogWindow, ShellDialogWindow>();
        services.AddTransient<ShellDialogViewModel>();

    }

    private async void OnExit(object sender, ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();
        _host = null;
    }

    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // TODO: Please log and handle the exception as appropriate to your scenario
        // For more info see https://docs.microsoft.com/dotnet/api/system.windows.application.dispatcherunhandledexception?view=netcore-3.0
    }
}
