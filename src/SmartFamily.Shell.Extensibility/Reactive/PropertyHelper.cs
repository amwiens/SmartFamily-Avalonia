using ReactiveUI;

namespace SmartFamily.Extensibility.Reactive
{
    public class PropertyHelper<TRet>
    {
        public PropertyHelper(IReactiveObject source, IObservable<TRet> observable, string propertyName)
        {
            observable.Subscribe(v =>
            {
                source.RaisePropertyChanging(propertyName);
                Value = v;
                source.RaisePropertyChanged(propertyName);
            });
        }

        public TRet Value { get; set; }
    }
}