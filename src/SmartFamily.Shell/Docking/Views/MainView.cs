using Dock.Model;

namespace SmartFamily.ViewModels.Views
{
    public class MainView : DockBase
    {
        public override IDockable? Clone()
        {
            return this;
        }
    }
}