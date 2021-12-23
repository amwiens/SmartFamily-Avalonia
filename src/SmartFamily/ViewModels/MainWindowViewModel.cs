using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;

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

        public MainWindowViewModel(IMessenger messenger)
            : base(messenger)
        {
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
        }

        public void Initialize()
        {

        }

        public void Terminate()
        {

        }



        internal void ShutdownSession()
        {

        }
    }
}
