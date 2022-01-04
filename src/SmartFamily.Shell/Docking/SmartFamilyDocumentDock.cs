using Dock.Model.Core;
using Dock.Model.ReactiveUI.Controls;

using SmartFamily.Shell;

namespace SmartFamily.Docking
{
    public class SmartFamilyDocumentDock : DocumentDock
    {
        public override bool OnClose()
        {
            ShellViewModel.Instance.RemoveDock(this);
            return base.OnClose();
        }

        public override IDockable Clone()
        {
            var result = base.Clone() as SmartFamilyDocumentDock;

            result.VisibleDockables = this.VisibleDockables;

            return result;
        }
    }
}