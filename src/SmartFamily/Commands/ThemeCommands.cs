using ReactiveUI;

using SmartFamily.Extensibility;
using SmartFamily.Extensibility.Theme;
using SmartFamily.Shell;

using System.Composition;

namespace SmartFamily.Commands
{
    internal class ThemeCommands
    {
        [ExportCommandDefinition("Theme.Light")]
        public CommandDefinition LightCommand { get; }

        [ExportCommandDefinition("Theme.Dark")]
        public CommandDefinition DarkCommand { get; }

        private IShell _shell;

        [ImportingConstructor]
        public ThemeCommands(CommandIconService commandIconService)
        {
            _shell = IoC.Get<IShell>();

            LightCommand = new CommandDefinition("Light", null, ReactiveCommand.Create(SetThemeLight));
            DarkCommand = new CommandDefinition("Dark", null, ReactiveCommand.Create(SetThemeDark));
        }

        public static void SetThemeLight()
        {
            GlobalSettings.Settings.SetSettings(new GeneralSettings() { Theme = ColorTheme.VisualStudioLight.Name });
            ColorTheme.LoadTheme(ColorTheme.VisualStudioLight);
        }

        public static void SetThemeDark()
        {
            GlobalSettings.Settings.SetSettings(new GeneralSettings() { Theme = ColorTheme.VisualStudioDark.Name });
            ColorTheme.LoadTheme(ColorTheme.VisualStudioDark);
        }
    }
}