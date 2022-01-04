using SmartFamily.Menus.Models;
using SmartFamily.Menus.ViewModels;

using System.Collections.Immutable;

namespace SmartFamily.Toolbars.ViewModels
{
    public class ToolbarViewModel : MenuViewModel
    {
        public string Name => _toolbar.Value.Name;

        private readonly Lazy<Toolbar> _toolbar;

        public ToolbarViewModel(Lazy<Toolbar> toolbar, IImmutableList<MenuItemModel> items)
            : base(items)
        {
            _toolbar = toolbar;
        }
    }
}