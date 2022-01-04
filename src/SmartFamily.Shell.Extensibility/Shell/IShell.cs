﻿using Avalonia.Controls;

using Dock.Model.Controls;
using Dock.Model;

using SmartFamily.Documents;
using SmartFamily.Extensibility.Dialogs;
using SmartFamily.MVVM;

namespace SmartFamily.Shell
{
    public interface IPerspective
    {
        void AddTool(IToolViewModel tool);

        void RemoveTool(IToolViewModel tool);

        void RemoveDock(IDock dock);

        IReadOnlyList<IToolViewModel> Tools { get; }

        IRootDock Root { get; }

        IToolViewModel SelectedTool { get; set; }
    }

    public interface IShell
    {
        IDocumentTabViewModel SelectedDocument { get; set; }

        void Select(object view);

        ModalDialogViewModelBase ModalDialog { get; set; }

        void AddDocument(IDocumentTabViewModel document, bool temporary = true, bool select = true);

        void RemoveDocument(IDocumentTabViewModel document);

        IPerspective MainPerspective { get; }

        IPerspective CreatePerspective();

        IPerspective CurrentPerspective { get; set; }

        IReadOnlyList<IDocumentTabViewModel> Documents { get; }

        IPanel Overlay { get; }
    }
}