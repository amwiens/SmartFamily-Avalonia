using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;

using Serilog;

using SmartFamily.Model;
using SmartFamily.Model.Settings;
using SmartFamily.Model.Settings.Json;
using SmartFamily.Views;

using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SmartFamily
{
    public class App : Application
    {
        public App()
        {
            Name = Global.AppName;
            System.Threading.Thread.CurrentThread.Name = $"{Global.AppName} UI Thread";
            Services = ConfigureServices();
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override async void OnFrameworkInitializationCompleted()
        {
            base.OnFrameworkInitializationCompleted();

            try
            {
                if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    desktop.ShutdownMode = ShutdownMode.OnExplicitShutdown;
                    await LoadSplashScreen().ConfigureAwait(true);

                    desktop.MainWindow = new MainWindow();
                    desktop.ShutdownMode = ShutdownMode.OnLastWindowClose;
                    desktop.MainWindow.Show();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "OnFrameworkInitializationCompleted");
                throw;
            }
        }

        private async Task LoadSplashScreen()
        {
            // Wait 1.5 seconds to make sure the splash screen is visible
#if DEBUG
            var minDisplayTime = TimeSpan.FromMilliseconds(3_000);
#else
            var minDisplayTime = TimeSpan.FromMilliseconds(1_500);
#endif

            // Show splash screen
            var splashScreen = new Views.Windows.SplashScreenWindow();
            splashScreen.Show();

            var sw = new Stopwatch();

            Log.Information("App.LoadSplashScreen: Loading settings and showing splash screen.");
            sw.Start();

            // Load what needs to be loaded

            // We need to get user settings before loading the UI
            await ((ISettingsManager)Services.GetService(typeof(ISettingsManager))).InitializeAsync().ConfigureAwait(true);

            // End of loading

            sw.Stop();
            Log.Information("App.LoadSplashScreen: Loading settings done in {ElapsedMilliseconds}ms.", sw.ElapsedMilliseconds);

            if (sw.Elapsed < minDisplayTime)
            {
                await Task.Delay(minDisplayTime - sw.Elapsed).ConfigureAwait(true);
            }

            splashScreen.Close();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use.
        /// </summary>
        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddLogging(c => c.AddSerilog(dispose: true));

            // Messenger
            services.AddSingleton<IMessenger, StrongReferenceMessenger>();

            // Settings
            services.AddSingleton<ISettingsManager, JsonSettingsManager>();

            // Results

            // Session

            // Api

            // Sessions

            //Viewmodels

            return services.BuildServiceProvider();
        }
    }
}