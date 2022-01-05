namespace SmartFamily.Extensibility.Settings
{
    public interface ISettingsManager
    {
        void RegisterSettingsDialog(string category, SettingsViewModel viewModel);
    }
}