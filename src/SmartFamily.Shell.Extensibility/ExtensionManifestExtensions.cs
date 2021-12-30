namespace SmartFamily.Extensibility
{
    public static class ExtensionManifestExtensions
    {
        public const string MefComponentsString = "mefComponents";

        public static IEnumerable<string> GetMefComponents(this IExtensionManifest extension)
            => extension.GetAssets(MefComponentsString);
    }
}