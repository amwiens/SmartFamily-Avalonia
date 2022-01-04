using Dock.Model.ReactiveUI.Controls;

using SmartFamily.Shell;

namespace SmartFamily.Docking
{
    public class SmartFamilyToolDock : ToolDock
    {
        public override bool OnClose()
        {
            ShellViewModel.Instance.RemoveDock(this);
            return base.OnClose();
        }
    }
}