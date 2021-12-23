using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace SmartFamily.Model.Messages
{
    public sealed class SessionOpenedMessage : ValueChangedMessage<bool>
    {
        public bool IsSuccess => Value;

        public string? Error { get; }

        /// <summary>
        /// Session opened successfully.
        /// </summary>
        public SessionOpenedMessage() : base(true)
        { }

        public SessionOpenedMessage(string error) : base(false)
        {
            Error = error;
        }
    }
}