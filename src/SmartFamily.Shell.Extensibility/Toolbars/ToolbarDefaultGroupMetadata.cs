using SmartFamily.Menus;

using System.ComponentModel;

namespace SmartFamily.Toolbars
{
    public class ToolbarDefaultGroupMetadata
    {
        public string ToolbarName { get; set; }
        public MenuPath Path { get; set; }

        [DefaultValue(50000)]
        public int DefaultOrder { get; set; }
    }
}