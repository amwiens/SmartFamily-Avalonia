using Avalonia.Markup.Xaml;

using SmartFamily.Shell.Controls;

namespace SmartFamily
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}