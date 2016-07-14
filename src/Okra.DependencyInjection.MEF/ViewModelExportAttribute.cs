using System;
using System.Composition;

namespace Okra.DependencyInjection.MEF
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ViewModelExportAttribute : ExportAttribute
    {
        // *** Constructors ***

        public ViewModelExportAttribute(Type type)
            : base("OkraViewModel", typeof(object))
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            this.PageName = type.FullName;
        }

        public ViewModelExportAttribute(string pageName)
            : base("OkraViewModel", typeof(object))
        {
            if (string.IsNullOrEmpty(pageName))
                throw new ArgumentException(Resources.Exception_ArgumentException_StringIsNullOrEmpty, nameof(pageName));

            this.PageName = pageName;
        }

        // *** Properties ***

        public string PageName { get; private set; }
    }
}
