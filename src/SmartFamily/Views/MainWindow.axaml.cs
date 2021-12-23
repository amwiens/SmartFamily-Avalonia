using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;

using Microsoft.Toolkit.Mvvm.Messaging;

using Serilog;

using SmartFamily.ViewModels;

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SmartFamily.Views
{
    public partial class MainWindow : Window
    {
        private readonly IMessenger _messenger;

        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            _messenger = (IMessenger)App.Current.Services.GetService(typeof(IMessenger));
            if (_messenger == null)
            {
                throw new ArgumentNullException("Could not fine 'IMessenger' service in 'App.Current.Services'.");
            }
            //_messenger.Register<MainWindow, ShowNewSessionWindowMessage>(this, async (r, _) => await r.ShowWindowDialog<NewSessionWindow>().ConfigureAwait(false));

            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnClosing(object? sender, CancelEventArgs e)
        {
            try
            {
                ViewModel?.ShutdownSession();
                ViewModel?.Terminate();

                if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    foreach (var window in desktop.Windows)
                    {
                        if (window is MainWindow) continue;
                        window.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "MainWindow.OnClosing");
            }
        }

        private void OnOpened(object sender, EventArgs e)
        {
            // Tell the viewModel we have loaded and we can process data
            ViewModel.Initialize();
        }

        private void OnClosed(object sender, EventArgs e)
        {
            // Nothing
        }

        private Task ShowWindowDialog<T>() where T : Window
        {
            return Dispatcher.UIThread.InvokeAsync(() =>
            {
                var window = Activator.CreateInstance<T>();
                window.ShowDialog(this);
            });
        }

        private static void OpenLink(string link)
        {
            try
            {
                // https://github.com/dotnet/runtime/issues/28005
                // not sure if works on every platform
                Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Arguments = $"/c start {link}"
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "MainWindow.OpenLink");
            }
        }

        private void ShowAboutButton_OnClick(object sender, RoutedEventArgs e)
        {
            //ShowWindowDialog<AboutWindow>();
        }

        private void BrowseSmartFamilyGithubMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            OpenLink("https://github.com/amwiens/SmartFamily-Avalonia");
        }


    }
}
