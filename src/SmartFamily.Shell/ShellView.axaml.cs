using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

using SmartFamily.Extensibility.Theme;

namespace SmartFamily.Shell
{
    public partial class ShellView : UserControl
    {
        public ShellView()
        {
            InitializeComponent();

            DataContext = ShellViewModel.Instance;

            ShellViewModel.Instance.Overlay = this.FindControl<Panel>("PART_ExtensibleOverlay");

            KeyBindings.AddRange(ShellViewModel.Instance.KeyBindings);

            ColorTheme.LoadTheme(ColorTheme.VisualStudioLight);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnAttachedToVisualTree(e);

            Focus();
        }
    }
}