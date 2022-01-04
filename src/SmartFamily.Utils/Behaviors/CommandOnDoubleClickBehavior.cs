﻿using Avalonia.Controls;
using Avalonia.Interactivity;

using System.Reactive.Disposables;

namespace SmartFamily.Utils.Behaviors
{
    public class CommandOnDoubleClickBehavior : CommandBasedBehavior<Control>
    {
        private CompositeDisposable _disposables;

        protected override void OnAttached()
        {
            _disposables = new CompositeDisposable();

            base.OnAttached();

            _disposables.Add(AssociatedObject.AddDisposableHandler(Control.PointerPressedEvent, (sender, e) =>
            {
                if (e.ClickCount == 2)
                {
                    e.Handled = ExecuteCommand();
                }
            }));
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            _disposables.Dispose();
        }
    }
}