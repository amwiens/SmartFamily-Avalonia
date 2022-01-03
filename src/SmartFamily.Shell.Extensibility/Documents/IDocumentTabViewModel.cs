using SmartFamily.MVVM;

namespace SmartFamily.Documents
{
    public interface IDocumentTabViewModel : IDockableViewModel
    {
        bool IsDirty { get; set; }
    }
}