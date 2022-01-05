using System.Collections.Specialized;
using System.ComponentModel;

namespace SmartFamily.Extensibility.MVVM
{
    public interface IObservableCollection<T> : IList<T>, INotifyPropertyChanged, INotifyCollectionChanged
    {
    }
}