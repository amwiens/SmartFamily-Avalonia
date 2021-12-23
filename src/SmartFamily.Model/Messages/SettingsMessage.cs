using Microsoft.Toolkit.Mvvm.Messaging.Messages;

using SmartFamily.Model.Settings;

namespace SmartFamily.Model.Messages
{
    public sealed class SettingsMessage : ValueChangedMessage<UserSettings>
    {
        public UserSettingsUpdate Type { get; }

        public SettingsMessage(UserSettings value, UserSettingsUpdate type) : base(value)
        {
            Type = type;
        }
    }

    public enum UserSettingsUpdate
    {
        Timezone = 0,
        SoundsActivated = 1
    }
}