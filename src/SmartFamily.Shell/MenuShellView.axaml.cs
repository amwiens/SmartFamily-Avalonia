using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SmartFamily.Shell
{
    public partial class MenuShellView : UserControl
    {
        public MenuShellView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}