using SmartFamily.Menus;

using System.Composition;

namespace SmartFamily.MainMenu
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Property)]
    internal class ExportMainMenuDefaultGroupAttribute : ExportAttribute
    {
        public MenuPath Path { get; }

        public ExportMainMenuDefaultGroupAttribute(params string[] path)
            : base(ExportContractNames.MainMenu, typeof(object))
        {
            Path = new MenuPath(path);
        }
    }
}