using Dock.Model.Controls;
using Dock.Model.Core;
using Dock.Model.ReactiveUI;
using Dock.Model.ReactiveUI.Controls;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFamily.Docking
{
    /// <inheritdoc/>
    public class DefaultLayoutFactory : Factory
    {
        private ObservableCollection<IDockable> _documents;

        public DefaultLayoutFactory()
        {
            _documents = new ObservableCollection<IDockable>();
        }

        public RootDock Root { get; private set; }

        public override IToolDock CreateToolDock()
        {
            return new SmartFamilyToolDock();
        }

        public override IDocumentDock CreateDocumentDock()
        {
            return new SmartFamilyDocumentDock();
        }

        /// <inheritdoc/>
        public override IRootDock CreateLayout()
        {
            var documentDock = new SmartFamilyDocumentDock
            {
                Id = "DocumentsPane",
                Proportion = double.NaN,
                Title = "DocumentsPane",
                ActiveDockable = null,
                IsCollapsable = false,
                VisibleDockables = _documents
            };

            var verticalContainer = new ProportionalDock
            {
                Id = "VerticalContainer",
                Proportion = double.NaN,
                Orientation = Orientation.Vertical,
                Title = "VerticalContainer",
                ActiveDockable = null,
                VisibleDockables = new ObservableCollection<IDockable>
                {
                    documentDock,
                }
            };

            var horizontalContainer = new ProportionalDock
            {
                Id = "HorizontalContainer",
                Proportion = double.NaN,
                Orientation = Orientation.Horizontal,
                Title = "HorizontalContainer",
                ActiveDockable = null,
                VisibleDockables = new ObservableCollection<IDockable>
                {
                    verticalContainer,
                }
            };


        }
    }
}