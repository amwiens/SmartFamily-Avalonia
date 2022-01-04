using SmartFamily.Menus;
using SmartFamily.Shell.Extensibility.Platforms;

using System.ComponentModel;

namespace SmartFamily.MainMenu
{
    public class MainMenuDefaultGroupMetadata
    {
        public MenuPath Path { get; set; }

        [DefaultValue(50000)]
        public int DefaultOrder { get; set; }

        [DefaultValue(true)]
        public bool ExportOnWindows { get; set; }

        [DefaultValue(true)]
        public bool ExportOnOsx { get; set; }

        [DefaultValue(true)]
        public bool ExportOnLinux { get; set; }

        public bool ExportOnCurrentPlatform
        {
            get
            {
                switch (Platform.PlatformIdentifier)
                {
                    case PlatformID.MacOSX:
                        return ExportOnOsx;

                    case PlatformID.Win32NT:
                        return ExportOnWindows;

                    case PlatformID.Unix:
                        return ExportOnLinux;

                    default:
                        return false;
                }
            }
        }
    }
}