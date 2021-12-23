using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;

using SmartFamily.Model;
using SmartFamily.Model.Messages;
using SmartFamily.Model.Sessions;
using SmartFamily.Model.Settings;

namespace SmartFamily.ViewModels
{
    public sealed class StatusViewModel : ObservableRecipient
    {
        public ISettingsManager SettingsManager { get; }

        public StatusViewModel(IMessenger messenger, ISettingsManager settingsManager)
            : base(messenger)
        {
            SettingsManager = settingsManager;

            Messenger.Register<StatusViewModel, SessionOpenedMessage>(this, (r, m) =>
            {
                if (m.IsSuccess)
                {
                    r.Progress = 0;
                    r.IsProgressIndeterminate = false;
                    r.OnPropertyChanged(nameof(IsSessionActive));
                }
                else
                {
                    // Error
                }
            });
        }

        private string _serverStatistics;
        public string ServerStatistics
        {
            get { return _serverStatistics; }
            set
            {
                if (_serverStatistics == value) return;

                _serverStatistics = value;
                OnPropertyChanged();
            }
        }

        private bool? _isLive;
        public bool? IsLive
        {
            get { return _isLive; }
            set
            {
                if (_isLive == value) return;
                _isLive = value;
                if (_isLive == false)
                {
                    // If we are in backtest, we override
                    SmartFamilySounds.CanPlaySounds = false;
                }
                OnPropertyChanged();
            }
        }

        private decimal _progress;
        public decimal Progress
        {
            get { return _progress; }
            set
            {
                if (_progress == value) return;

                _progress = value;
                OnPropertyChanged();
            }
        }

        private string _sessionName;
        public string SessionName
        {
            get { return _sessionName; }
            set
            {
                if (_sessionName == value) return;

                _sessionName = value;
                OnPropertyChanged();
            }
        }

        private string _projectName;
        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                if (_projectName == value) return;

                _projectName = value;
                OnPropertyChanged();
            }
        }

        private bool _isProgressIndeterminate;
        public bool IsProgressIndeterminate
        {
            get { return _isProgressIndeterminate; }
            set
            {
                if (_isProgressIndeterminate == value) return;

                _isProgressIndeterminate = value;
                OnPropertyChanged();
            }
        }

        private SessionState _sessionState = SessionState.Unsubscribed;
        public SessionState SessionState
        {
            get { return _sessionState; }
            set
            {
                if (_sessionState == value) return;
                _sessionState = value;

                // Handle disconnection in Live mode
                if (IsLive == true && _sessionState == SessionState.Unsubscribed)
                {
                    IsProgressIndeterminate = false;
                    //AlgorithmStatus = null;
                    var utcNow = DateTime.UtcNow;
                    var message = $"⚠ Live session timed out at UTC {utcNow}.";
                    ServerStatistics = message;
                    // TODO: Message should not be sent from view model, need to handle that in the model
                    Messenger.Send(new LogEntryReceivedMessage(utcNow, message, LogItemType.Monitor));
                }

                OnPropertyChanged();
            }
        }

        //private AlgorithmStatus? _algorithmStatus;
        //public AlgorithmStatus? AlgorithmStatus
        //{
        //    get { return _algorithmStatus; }
        //    set
        //    {
        //        if (_algorithmStatus == value) return;

        //        _algorithmStatus = value;
        //        OnPropertyChanged();
        //    }
        //}

        public bool IsSessionActive => true; //_sessionService.IsSessionActive;

        private void ProcessServerStatistics(IDictionary<string, string> serverStatistics)
        {
            if (serverStatistics == null || serverStatistics.Count == 0) return;
            ServerStatistics = $"Server: {string.Join(", ", serverStatistics.OrderBy(kvp => kvp.Key).Select(kvp => $"{kvp.Key}: {kvp.Value}"))}";
        }
    }
}