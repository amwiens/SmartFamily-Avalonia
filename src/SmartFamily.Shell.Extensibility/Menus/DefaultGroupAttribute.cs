using System.Composition;

namespace SmartFamily.Menus
{
    [MetadataAttribute]
    public class DefaultGroupAttribute : Attribute
    {
        public string DefaultGroup { get; }

        public DefaultGroupAttribute(string name)
        {
            DefaultGroup = name;
        }
    }
}