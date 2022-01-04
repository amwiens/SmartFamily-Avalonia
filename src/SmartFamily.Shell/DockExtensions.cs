using Dock.Model.Controls;
using Dock.Model.Core;

using ReactiveUI;

using SmartFamily.Documents;
using SmartFamily.MVVM;

namespace SmartFamily.Shell
{
    abstract class SmartFamilyTab<T> : ViewModel, IDockable where T : IDockableViewModel
    {
        T _model;

        public SmartFamilyTab(T model)
        {
            _model = model;
            Context = model;
            Id = "SFTab";

            model.WhenAnyValue(x => x.Title)
                .Subscribe(title => Title = title);
        }

        public string Id { get; set; }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { this.RaiseAndSetIfChanged(ref _title, value); }
        }

        private object _context;

        public object Context
        {
            get { return _context; }
            set { this.RaiseAndSetIfChanged(ref _context, value); }
        }

        public IDockable Owner { get; set; }
        public IFactory Factory { get; set; }

        public bool OnClose()
        {
            return _model.OnClose();
        }

        public void OnOpen()
        {
            _model.OnOpen();
        }

        public void OnSelected()
        {

        }

        public IDockable Clone()
        {
            return this;
        }
    }

    class SmartFamilyDocumentTab : SmartFamilyTab<IDocumentTabViewModel>, IDocument
    {
        public SmartFamilyDocumentTab(IDocumentTabViewModel model) : base(model)
        {

        }
    }

    class SmartFamilyToolTab : SmartFamilyTab<IToolViewModel>, ITool
    {
        public SmartFamilyToolTab(IToolViewModel model) : base(model)
        {

        }
    }

    public static class DockExtensions
    {
        public static IDockable Dock(this IDock dock, IDockableViewModel model, bool add = true)
        {
            IDockable currentTab = null;

            if (add)
            {
                if (model is IToolViewModel toolModel)
                {
                    currentTab = new SmartFamilyToolTab(toolModel);
                }
                else if (model is IDocumentTabViewModel documentModel)
                {
                    currentTab = new SmartFamilyDocumentTab(documentModel);
                }

                dock.Factory.AddDockable(dock, currentTab);
            }
            else
            {
                currentTab = dock.VisibleDockables.FirstOrDefault(v => v.Context == model);

                if (currentTab != null)
                {
                    dock.Factory.UpdateDockable(currentTab, currentTab.Owner);
                }
            }

            return currentTab;
        }
    }
}