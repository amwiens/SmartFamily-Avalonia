﻿using ReactiveUI;

using SmartFamily.Extensibility.Dialogs;

using System.Diagnostics;
using System.Reactive;
using System.Reflection;

namespace SmartFamily
{
    public class AboutDialogViewModel : ModalDialogViewModelBase
    {
        public AboutDialogViewModel() : base("About", true, false)
        {
            OKCommand = ReactiveCommand.Create(() => Close());
        }

        public override ReactiveCommand<Unit, Unit> OKCommand { get; protected set; }

        public string Version => FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).FileVersion;
    }
}