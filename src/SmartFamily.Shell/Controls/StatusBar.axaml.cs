using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SmartFamily.Shell.Controls
{
    public partial class StatusBar : UserControl
    {
        public StatusBar()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}