using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;

using SmartFamily.Extensibility.Theme;
using SmartFamily.GlobalSettings;
using SmartFamily.Shell;

using System;

namespace SmartFamily
{
    public class App : Application
    {
        [STAThread]
        private static void Main(string[] args)
        {
            BuildAvaloniaApp().StartShellApp("SmartFamilyApp", AppMain, args);
        }

        private static void AppMain(string[] args)
        {
            var generalSettings = Settings.GetSettings<GeneralSettings>();
            Dispatcher.UIThread.InvokeAsync(() => { ColorTheme.LoadTheme(generalSettings.Theme); });
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>().UsePlatformDetect();

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}