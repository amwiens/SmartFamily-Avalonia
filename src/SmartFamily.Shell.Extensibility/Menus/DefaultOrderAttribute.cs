using System.Composition;

namespace SmartFamily.Menus
{
    [MetadataAttribute]
    public class DefaultOrderAttribute : Attribute
    {
        public int DefaultOrder { get; }

        public DefaultOrderAttribute(int order)
        {
            DefaultOrder = order;
        }
    }
}