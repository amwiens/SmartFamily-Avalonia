using SmartFamily.ViewModels;

namespace SmartFamily
{
    internal sealed class ViewModelLocator
    {
        public static MainWindowViewModel MainWindow => GetViewModel<MainWindowViewModel>();

        private static T GetToolViewModel<T>() where T : ToolPaneViewModel
        {
            //return _container.GetInstance<T>();
            return (T)App.Current.Services.GetService(typeof(T));
        }

        private static T GetViewModel<T>()
        {
            // Get all viewmodels as unique instances
            //return _container.GetInstance<T>();
            return (T)App.Current.Services.GetService(typeof(T));
        }
    }
}