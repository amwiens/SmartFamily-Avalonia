namespace SmartFamily.Extensibility
{
    public interface IExtensionManager
    {
        IEnumerable<IExtensionManifest> GetInstalledExtensions();
    }
}