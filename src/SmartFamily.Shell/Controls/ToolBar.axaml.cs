using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SmartFamily.Shell.Controls
{
    public partial class ToolBar : UserControl
    {
        public ToolBar()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}