﻿using ReactiveUI;

using System.Reactive;

namespace SmartFamily.Extensibility.Dialogs
{
    public class ModalDialogViewModelBase : ReactiveObject
    {
        private bool cancelButtonVisible;

        private bool isVisible;

        private bool okayButtonVisible;

        private string title;

        private TaskCompletionSource<bool> dialogCloseCompletionSource;

        public ModalDialogViewModelBase(string title, bool okayButton = true, bool cancelButton = true)
        {
            OKButtonVisible = okayButton;
            CancelButtonVisible = cancelButton;

            isVisible = false;
            this.title = title;

            CancelCommand = ReactiveCommand.Create(() => Close(false));
        }

        public bool CancelButtonVisible
        {
            get { return cancelButtonVisible; }
            set { this.RaiseAndSetIfChanged(ref cancelButtonVisible, value); }
        }

        public bool OKButtonVisible
        {
            get { return okayButtonVisible; }
            set { this.RaiseAndSetIfChanged(ref okayButtonVisible, value); }
        }

        public virtual ReactiveCommand<Unit, Unit> OKCommand { get; protected set; }
        public ReactiveCommand<Unit, Unit> CancelCommand { get; }

        public string Title
        {
            get { return title; }
            private set { this.RaiseAndSetIfChanged(ref title, value); }
        }

        public bool IsVisible
        {
            get { return isVisible; }
            set { this.RaiseAndSetIfChanged(ref isVisible, value); }
        }

        public Task<bool> ShowDialogAsync()
        {
            isVisible = true;

            dialogCloseCompletionSource = new TaskCompletionSource<bool>();

            OnOpen();

            return dialogCloseCompletionSource.Task;
        }

        public void Close(bool success = true)
        {
            IsVisible = false;

            OnClose();

            dialogCloseCompletionSource.SetResult(success);
        }

        public virtual void OnClose()
        {
        }

        public virtual void OnOpen()
        {
        }
    }
}