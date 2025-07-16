using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.Logging;
using Serilog;

namespace WPFApp
{

    public partial class App : Application
    {
        public static ILoggerFactory LoggerFactory { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        LoggerFactory = new LoggerFactory()
            .AddSerilog();

        Log.Information("🔧 Application started");

        base.OnStartup(e);
    }
        protected override void OnExit(ExitEventArgs e)
        {
            Log.CloseAndFlush(); // Đảm bảo log được flush khi thoát app
            base.OnExit(e);
        }
    }

}
