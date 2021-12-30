using SmartFamily.Extensibility;

using System.Composition.Convention;
using System.Composition.Hosting;
using System.Reflection;

namespace SmartFamily
{
    public static class CompositionRoot
    {
        public static CompositionHost CreateContainer(ExtensionManager extensionManager)
        {
            var conventions = new ConventionBuilder();
            conventions.ForTypesDerivedFrom<IExtension>();

            // TODO: AppDomain here is a custom appdomain from namespace SmartFamily.Extensibility.Utils. It is able
            // to load any assembly in the bin directory (so not really appdomain) we need to get rid of this
            // once all our default extensions are published with a manifest and copied to extensions dir.
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Distinct();

            var extensionAssemblies = LoadMefComponents(extensionManager);

            var configuration = new ContainerConfiguration()
                .WithAssemblies(assemblies, conventions)
                .WithAssemblies(extensionAssemblies);
            return configuration.CreateContainer();
        }

        private static IEnumerable<Assembly> LoadMefComponents(ExtensionManager extensionManager)
        {
            var assemblies = new List<Assembly>();

            foreach (var extension in extensionManager.GetInstalledExtensions())
            {
                foreach (var mefComponent in extension.GetMefComponents())
                {
                    try
                    {
                        assemblies.Add(Assembly.LoadFrom(mefComponent));
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine($"Failed to load MEF component from extension: '{mefComponent}'");
                        System.Console.WriteLine(ex.ToString());
                    }
                }
            }

            return assemblies;
        }
    }
}