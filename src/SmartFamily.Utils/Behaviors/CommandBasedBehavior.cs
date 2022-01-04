﻿using Avalonia;
using Avalonia.Xaml.Interactivity;

using System.Windows.Input;

namespace SmartFamily.Utils.Behaviors
{
    public class CommandBasedBehavior<T> : Behavior<T> where T : AvaloniaObject
    {
        private ICommand _command;

        /// <summary>
        /// Defines the <see cref="Command"/> property.
        /// </summary>
        public static readonly DirectProperty<CommandBasedBehavior<T>, ICommand> CommandProperty =
            AvaloniaProperty.RegisterDirect<CommandBasedBehavior<T>, ICommand>(nameof(Command),
                commandBehavior => commandBehavior.Command, (commandBehavior, command) => commandBehavior.Command = command, enableDataValidation: true);

        /// <summary>
        /// Defines the <see cref="CommandParameter"/> property.
        /// </summary>
        public static readonly StyledProperty<object> CommandParameterProperty =
            AvaloniaProperty.Register<CommandBasedBehavior<T>, object>(nameof(CommandParameter));

        /// <summary>
        /// Gets or sets an <see cref="ICommand"/> to be invoked when the button is clicked.
        /// </summary>
        public ICommand Command
        {
            get { return _command; }
            set { SetAndRaise(CommandProperty, ref _command, value); }
        }

        /// <summary>
        /// Gets or sets a parameter to be passed to the <see cref="Command"/>.
        /// </summary>
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        protected bool ExecuteCommand()
        {
            if (Command != null)
            {
                Command.Execute(CommandParameter);
                return true;
            }

            return false;
        }
    }
}