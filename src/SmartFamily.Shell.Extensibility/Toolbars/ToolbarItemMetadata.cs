using SmartFamily.Menus;

using System.ComponentModel;

namespace SmartFamily.Toolbars
{
    public class ToolbarItemMetadata
    {
        public string ToolbarName { get; set; }
        public MenuPath Path { get; set; }

        [DefaultValue(null)]
        public string DefaultGroup { get; set; }

        [DefaultValue(50000)]
        public int DefaultOrder { get; set; }
    }
}