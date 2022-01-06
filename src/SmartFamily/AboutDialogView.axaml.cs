using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SmartFamily
{
    public partial class AboutDialogView : UserControl
    {
        public AboutDialogView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
