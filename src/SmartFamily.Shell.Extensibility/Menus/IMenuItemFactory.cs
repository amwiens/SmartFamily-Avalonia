using Avalonia.Media;

namespace SmartFamily.Menus
{
    public interface IMenuItemFactory
    {
        IMenuItem CreateCommandMenuItem(string commandName);
        IMenuItem CreateHeaderMenuItem(string label, DrawingGroup icon);
    }
}