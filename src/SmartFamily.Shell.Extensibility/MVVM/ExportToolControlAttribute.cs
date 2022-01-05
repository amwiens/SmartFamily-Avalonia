using System.Composition;

namespace SmartFamily.MVVM
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportToolControlAttribute : ExportAttribute
    {
        public ExportToolControlAttribute()
            : base(typeof(ToolViewModel))
        {
        }
    }
}