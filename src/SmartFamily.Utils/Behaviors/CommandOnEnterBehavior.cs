﻿using Avalonia.Controls;
using Avalonia.Interactivity;

using System.Reactive.Disposables;

namespace SmartFamily.Utils.Behaviors
{
    public class CommandOnEnterBehavior : CommandBasedBehavior<TextBox>
    {
        private CompositeDisposable _disposables;

        protected override void OnAttached()
        {
            _disposables = new CompositeDisposable();

            base.OnAttached();

            _disposables.Add(AssociatedObject.AddDisposableHandler(TextBox.KeyDownEvent, (sender, e) =>
            {
                if (e.Key == Avalonia.Input.Key.Enter)
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