namespace SmartFamily.MVVM
{
    public enum Location
    {
        None,
        Left,
        Right,
        Bottom,
        Top,
    }

    public abstract class ViewModel : ViewModel<object>
    {
        protected ViewModel() : base(null)
        {
        }
    }
}