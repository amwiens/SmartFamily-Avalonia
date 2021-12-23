using SmartFamily.Model.Sessions;

namespace SmartFamily.Model.Settings
{
    public interface ISettingsManager
    {
        /// <summary>
        /// Initialize the <see cref="ISettingsManager"/> and load the settings.
        /// </summary>
        Task InitializeAsync();

        /// <summary>
        /// Save the <see cref="ISettingsManager"/> settings.
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();

        /// <summary>
        /// Update the datagrid's columns settings.
        /// </summary>
        /// <param name="name">The datagrid's name.</param>
        /// <param name="columns">The columns to save.</param>
        Task UpdateGridAsync(string name, IReadOnlyList<Tuple<string, int>> columns);

        /// <summary>
        /// gets the datagrid's columns settings.
        /// </summary>
        /// <param name="name">The datagrid's name.</param>
        Task<IReadOnlyList<Tuple<string, int>>> GetGridAsync(string name);

        /// <summary>
        /// Activate/Deactivate sounds.
        /// </summary>
        void SetSoundsActivated(bool enable);

        /// <summary>
        /// Convert to selected timezone.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> to convert.</param>
        DateTime ConvertToSelectedTimezone(DateTime dateTime);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        int GetPlotRefreshLimitMilliseconds();

        /// <summary>
        /// Checks the settings version against the app version.
        /// </summary>
        void CheckVersion();

        void UpdateSessionParameters(ISessionParameters sessionParameters);

        bool IsInitialized { get; }

        IEnumerable<string> SetupGridsColumns { get; }

        TimeZoneInfo SelectedTimeZone { get; set; }

        IDictionary<string, string> SessionParameters { get; }

        bool SoundsActivated { get; }

        UserSettings UserSettings { get; }
    }
}