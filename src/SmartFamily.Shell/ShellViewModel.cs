﻿using Avalonia.Input;

using Dock.Model.Controls;
using Dock.Model.Core;

using ReactiveUI;

using SmartFamily.Commands;
using SmartFamily.Docking;
using SmartFamily.Documents;
using SmartFamily.Extensibility;
using SmartFamily.Extensibility.Dialogs;
using SmartFamily.Extensibility.Shell;
using SmartFamily.MainMenu;
using SmartFamily.Menus.ViewModels;
using SmartFamily.MVVM;
using SmartFamily.Shell.Controls;
using SmartFamily.Shell.Extensibility.Platforms;
using SmartFamily.Toolbars;
using SmartFamily.Toolbars.ViewModels;

using System.Composition;

namespace SmartFamily.Shell
{
    [Export(typeof(ShellViewModel))]
    [Export(typeof(IShell))]
    [Shared]
    public class ShellViewModel : ViewModel, IShell
    {
        public static ShellViewModel Instance { get; set; }
        private List<KeyBinding> _keyBindings;
        private IPerspective _currentPerspective;
        private IDisposable _selectedDockableChangedSubscription;

        private IDocumentTabViewModel _selectedDocument;

        private IEnumerable<Lazy<IExtension>> _extensions;
        private IEnumerable<Lazy<ToolViewModel>> _toolControls;
        private CommandService _commandService;
        private Dictionary<IDocumentTabViewModel, IDockable> _documentViews;
        private List<IDocumentTabViewModel> _documents;
        private List<IPerspective> _perspectives;

        private Lazy<StatusBarViewModel> _statusBar;

        private ModalDialogViewModelBase modalDialog;

        private IFactory _factory;
        private IRootDock _root;
        private IDock _layout;

        [ImportingConstructor]
        public ShellViewModel(
            CommandService commandService,
            Lazy<StatusBarViewModel> statusBar,
            MainMenuService mainMenuService,
            ToolbarService toolbarService,
            [ImportMany] IEnumerable<Lazy<IExtension>> extensions,
            [ImportMany] IEnumerable<Lazy<ToolViewModel>> toolControls)
        {
            _extensions = extensions;
            _toolControls = toolControls;

            _commandService = commandService;

            MainMenu = mainMenuService.GetMainMenu();

            var toolbars = toolbarService.GetToolbars();
            StandardToolbar = toolbars.SingleOrDefault(t => t.Key == "Standard").Value;

            _statusBar = statusBar;

            _keyBindings = new List<KeyBinding>();

            ModalDialog = new ModalDialogViewModelBase("Dialog");

            _documents = new List<IDocumentTabViewModel>();
            _documentViews = new Dictionary<IDocumentTabViewModel, IDockable>();
            _perspectives = new List<IPerspective>();

            this.WhenAnyValue(x => x.CurrentPerspective).Subscribe(perspective =>
            {
                if (perspective != null)
                {
                    //Root.Navigate(perspective.Root);
                    ApplyPerspective(perspective.Root);
                }
            });
        }

        public void RemoveDock(IDock dock)
        {
            CurrentPerspective.RemoveDock(dock);
        }

        public void Initialize(IFactory layoutFactory = null)
        {
            if (layoutFactory == null)
            {
                Factory = new DefaultLayoutFactory();
            }
            else
            {
                Factory = layoutFactory;
            }

            LoadLayout();

            foreach (var extension in _extensions)
            {
                if (extension.Value is IActivatableExtension activatable)
                {
                    activatable.BeforeActivation();
                }
            }

            foreach (var extension in _extensions)
            {
                if (extension.Value is IActivatableExtension activatable)
                {
                    activatable.Activation();
                }
            }

            foreach (var command in _commandService.GetKeyGesture())
            {
                if (command.Value != null)
                {
                    _keyBindings.Add(new KeyBinding { Command = command.Key.Command, Gesture = KeyGesture.Parse(command.Value) });
                }
            }

            IoC.Get<IStatusBar>().ClearText();
        }

        public string Title => Platform.AppName;

        public IReadOnlyList<IDocumentTabViewModel> Documents => _documents.AsReadOnly();

        public IRootDock Root
        {
            get => _root;
            set => this.RaiseAndSetIfChanged(ref _root, value);
        }

        public void LoadLayout()
        {
            //string path = System.IO.Path.Combine(Platform.SettingsDirectory, "Layout.json");

            //if (DockSerializer.Exists(path))
            //{
            //    //Layout = DockSerializer.Load<RootDock>(path);
            //}

            _layout = Factory.CreateLayout();
            Factory.InitLayout(_layout);

            Factory.SetActiveDockable(_layout.VisibleDockables.First());

            Root = _layout as IRootDock;

            MainPerspective = CreateInitialPerspective();

            _documentDock = Root.Factory.FindDockable(Root, x => x.Id == "DocumentsPane") as IDock;

            CurrentPerspective = MainPerspective;
        }

        public IPerspective MainPerspective { get; private set; }

        public IFactory Factory
        {
            get => _factory;
            set => this.RaiseAndSetIfChanged(ref _factory, value);
        }

        public void CloseLayout()
        {
            //Layout.Close();
        }

        public void SaveLayout()
        {
            //string path = System.IO.Path.combine(Platform.SettingsDirectory, "Layout.json");
            //DockSerializer.Save(path, Layout);
        }

        public MenuViewModel MainMenu { get; }

        public StatusBarViewModel StatusBar => _statusBar.Value;

        private ToolbarViewModel StandardToolbar { get; }

        public IEnumerable<KeyBinding> KeyBindings => _keyBindings;

        public IRootDock CreatePerspective(IDock dock)
        {
            if (dock != null && dock.Owner is IDock owner)
            {
                var clone = (IRootDock)dock.Clone();

                if (clone != null)
                {
                    owner.Factory.AddDockable(owner, clone);
                    //ApplyPerspective(clone);
                }

                return clone;
            }

            throw new Exception();
        }

        public void ApplyPerspective(IRootDock dock)
        {
            if (dock != null)
            {
                if (Root is IDock root)
                {
                    root.Navigate(dock);
                    root.Factory.SetFocusedDockable(root, dock);
                    root.DefaultDockable = dock;

                    _selectedDockableChangedSubscription?.Dispose();

                    _selectedDockableChangedSubscription = dock.WhenAnyValue(x => x.FocusedDockable)
                        .Subscribe(x =>
                        {
                            if (x?.Context is IDocumentTabViewModel doc)
                            {
                                SelectedDocument = doc;
                            }
                            else
                            {
                                SelectedDocument = null;
                            }
                        });
                }
            }
        }

        public IPerspective CreateInitialPerspective()
        {
            var result = new SmartFamilyPerspective(Root.ActiveDockable as IRootDock);

            _perspectives.Add(result);

            return result;
        }

        public IPerspective CreatePerspective()
        {
            var currentLayout = Root.ActiveDockable as IRootDock;
            var root = CreatePerspective(currentLayout);

            var result = new SmartFamilyPerspective(root);

            _perspectives.Add(result);

            return result;
        }

        public IPerspective CurrentPerspective
        {
            get => _currentPerspective;
            set => this.RaiseAndSetIfChanged(ref _currentPerspective, value);
        }

        private IDock _documentDock;

        public void AddDocument(IDocumentTabViewModel document, bool temporary = false, bool select = true)
        {
            if (!_documents.Contains(document))
            {
                _documents.Add(document);

                var view = _documentDock.Dock(document, true);

                _documentViews.Add(document, view);
            }

            if (select && _documentViews.ContainsKey(document))
            {
                Factory.SetActiveDockable(_documentViews[document]);

                document.OnOpen();

                SelectedDocument = document;
            }
        }

        public void RemoveDocument(IDocumentTabViewModel document)
        {
            if (document == null)
            {
                return;
            }

            if (_documentViews[document].Owner is IDock dock)
            {
                dock.VisibleDockables.Remove(_documentViews[document]);
                dock.Factory.UpdateDockable(_documentViews[document], dock);
            }

            _documentViews.Remove(document);
            _documents.Remove(document);

            if (SelectedDocument == document)
            {
                SelectedDocument = null;
            }

            GC.Collect();
        }

        public  ModalDialogViewModelBase ModalDialog
        {
            get { return modalDialog; }
            set { this.RaiseAndSetIfChanged(ref modalDialog, value); }
        }

        public IDocumentTabViewModel SelectedDocument
        {
            get => _selectedDocument;
            set
            {
                (_selectedDocument as IDocumentTabViewModel)?.OnDeselected();

                if (value != null && _documentViews.ContainsKey(value))
                {
                    foreach (var perspective in _perspectives)
                    {
                        if (_documentViews[value].Owner is IDock dock)
                        {
                            if (dock.VisibleDockables.Contains(_documentViews[value]))
                            {
                                dock.ActiveDockable = _documentViews[value];
                            }
                        }
                    }
                }

                _selectedDocument = value;

                _selectedDocument?.OnSelected();

                this.RaisePropertyChanged(nameof(SelectedDocument));
            }
        }

        public void Select(object view)
        {
            if (view is IDocumentTabViewModel doc)
            {
                SelectedDocument = doc;
            }
            else if (view is IToolViewModel tool)
            {
                CurrentPerspective.SelectedTool = tool;
            }
        }

        public void AddOrSelectDocument<T>(T document) where T : IDocumentTabViewModel
        {
            IDocumentTabViewModel doc = Documents.FirstOrDefault(x => x.Equals(document));

            if (doc != null)
            {
                SelectedDocument = doc;
            }
            else
            {
                AddDocument(document);
            }
        }

        public void AddOrSelectDocument<T>(Func<T> factory) where T : IDocumentTabViewModel
        {
            IDocumentTabViewModel doc = Documents.FirstOrDefault(x => x is T);

            if (doc != default)
            {
                SelectedDocument = doc;
            }
            else
            {
                AddDocument(factory());
            }
        }

        public T GetOrCreate<T>() where T : IDocumentTabViewModel, new()
        {
            T document = default;

            IDocumentTabViewModel doc = Documents.FirstOrDefault(x => x is T);

            if (doc != default)
            {
                document = (T)doc;
                SelectedDocument = doc;
            }
            else
            {
                document = new T();
                AddDocument(document);
            }
            return document;
        }

        public IDocumentTabViewModel GetOrCreate(Type type, Func<IDocumentTabViewModel> create)
        {
            IDocumentTabViewModel document = default;

            IDocumentTabViewModel doc = Documents.FirstOrDefault(x => x.GetType().IsAssignableFrom(type));

            if (doc != default)
            {
                document = doc;
                SelectedDocument = doc;
            }
            else
            {
                document = create();
                AddDocument(document);
            }
            return document;
        }

        public Avalonia.Controls.IPanel Overlay { get; internal set; }
    }
}