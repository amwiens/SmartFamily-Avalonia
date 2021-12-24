using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;

using Serilog;

using SmartFamily.Model;

using System;
using System.Collections.Generic;
using System.Text;

namespace SmartFamily.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        //private readonly ISessionService _sessionService;
        //private readonly ILayoutManager _layoutManager;

        private readonly DispatcherTimer _timer = new DispatcherTimer();

        public RelayCommand ExitCommand { get; }
        public RelayCommand OpenFileCommand { get; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand ExportCommand { get; }

        public RelayCommand OptionsCommand { get; }

        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public StatusViewModel StatusViewModel { get; }

        public SettingsViewModel SettingsViewModel { get; }

        public MainWindowViewModel(IMessenger messenger, StatusViewModel statusViewModel,
            SettingsViewModel settingsViewModel)
            : base(messenger)
        {
            StatusViewModel = statusViewModel;
            SettingsViewModel = settingsViewModel;

            Title = $"{Global.AppName} - {Global.AppVersion} - {Global.MachineName}";

#if DEBUG
            Title = "[DEBUG] " + Title;
#endif

            ExitCommand = new RelayCommand(() =>
            {
                if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    desktop.MainWindow.Close();
                }
            });

            //CloseCommand = new RelayCommand(() => _
            //OpenFileCommand = new RelayCommand(() => Messenger.Send(new ShowOpenFileWindowMessage()));
            //ExportCommand = new RelayCommand(Export, () => IsSessionActive && false);

            OptionsCommand = new RelayCommand(() =>
            {
                Log.Information("Show Options window.");
                new Views.Windows.SettingsWindow().Show();
            });
        }

        public void Initialize()
        {
        }

        public void Terminate()
        {
            SettingsViewModel.SettingsManager.SaveAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private DateTime _currentDateTimeUtc;

        public DateTime currentDateTimeUtc
        {
            get
            {
                return _currentDateTimeUtc;
            }

            private set
            {
                if (_currentDateTimeUtc == value) return;

                if (value.Kind != DateTimeKind.Utc)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Should be provided with UTC date, receied {value.Kind}.");
                }
                _currentDateTimeUtc = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentDateTimeLocal));
            }
        }

        public DateTime CurrentDateTimeLocal
        {
            get
            {
                return SettingsViewModel.SettingsManager.ConvertToSelectedTimezone(_currentDateTimeUtc);
            }
        }

        public string SelectedTimeZone
        {
            get
            {
                return SettingsViewModel.SettingsManager.SelectedTimeZone.DisplayName;
            }
        }

        internal void ShutdownSession()
        {
        }
    }
}