using Avalonia.Media;

using System.Windows.Input;

namespace SmartFamily.Menus.Models
{
    public class MenuItemSeparatorModel : MenuItemModel
    {
        private static readonly Lazy<IMenuItem> EmptyItem = new Lazy<IMenuItem>(() => new EmptyMenuItem());

        public MenuItemSeparatorModel()
            : base(EmptyItem, null)
        {

        }

        private class EmptyMenuItem : IMenuItem
        {
            public string Label => "-";
            public DrawingGroup Icon => null;

            public ICommand Command => null;

            public string Gesture => null;
        }
    }
}