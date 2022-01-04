using Avalonia.Markup.Xaml;

namespace SmartFamily.Shell.Controls
{
    public partial class ModalDialog : MetroWindow
    {
        public ModalDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}