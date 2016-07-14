using System;
using System.Composition;

namespace Okra.DependencyInjection.MEF
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ViewExportAttribute : ExportAttribute
    {
        // *** Constructors ***

        public ViewExportAttribute(Type type)
            : base("OkraView", typeof(object))
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            this.PageName = type.FullName;
        }

        public ViewExportAttribute(string pageName)
            : base("OkraView", typeof(object))
        {
            if (string.IsNullOrEmpty(pageName))
                throw new ArgumentException(Resources.Exception_ArgumentException_StringIsNullOrEmpty, nameof(pageName));

            this.PageName = pageName;
        }

        // *** Properties ***

        public string PageName { get; private set; }
    }
}
