using Avalonia;
using Avalonia.Controls;
using Avalonia.ReactiveUI;

using Dock.Model.Core;

using SmartFamily.Extensibility;
using SmartFamily.Shell.Extensibility.Platforms;

namespace SmartFamily.Shell
{
    public static class Shell
    {
        public delegate void ShellAppMainDelegate(string[] args);

        public static void StartShellApp<TAppBuilder>(this TAppBuilder builder, string appName, ShellAppMainDelegate main, string[] args, IFactory layoutFactory = null)
            where TAppBuilder : AppBuilderBase<TAppBuilder>, new()
        {
            builder
                .UseReactiveUI()
                .AfterSetup(_ =>
                {
                    Platform.AppName = appName;
                    Platform.Initialize();

                    var extensionManager = new ExtensionManager();
                    var container = CompositionRoot.CreateContainer(extensionManager);

                    IoC.Initialize(container);

                    ShellViewModel.Instance = IoC.Get<ShellViewModel>();

                    ShellViewModel.Instance.Initialize(layoutFactory);

                    main(args);
                }).StartWithClassicDesktopLifetime(args, ShutdownMode.OnMainWindowClose);
        }
    }
}