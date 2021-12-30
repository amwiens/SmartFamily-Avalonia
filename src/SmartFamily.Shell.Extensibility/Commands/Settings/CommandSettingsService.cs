﻿using SmartFamily.Shell.Extensibility.Platforms;
using SmartFamily.Utils;

using System.Composition;

namespace SmartFamily.Commands.Settings
{
    [Export]
    [Shared]
    public class CommandSettingsService
    {
        private const string CommandSettingsFileName = "CommandSettings.json";

        private static readonly string CommandSettingsFilePath = Path.Combine(Platform.SettingsDirectory, CommandSettingsFileName);

        private readonly Lazy<CommandSettings> _commandSettings;

        public CommandSettingsService()
        {
            _commandSettings = new Lazy<CommandSettings>(LoadCommandSettings);
        }

        public CommandSettings GetCommandSettings() => _commandSettings.Value;

        private CommandSettings LoadCommandSettings()
        {
            if (File.Exists(CommandSettingsFilePath))
            {
                return SerializedObject.Deserialize<CommandSettings>(CommandSettingsFilePath)
                    ?? new CommandSettings();
            }

            return new CommandSettings();
        }

        public void SaveCommandSettings() => SerializedObject.Serialize(CommandSettingsFilePath, _commandSettings.Value);
    }
}