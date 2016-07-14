using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting.Core;

namespace Okra.DependencyInjection.MEF
{
    public class ViewFactory : IViewFactory
    {
        private readonly CompositionContext _compositionContext;

        public ViewFactory(CompositionContext compositionContext)
        {
            _compositionContext = compositionContext;
        }
        
        public bool TryCreateView(string pageName, out object view)
        {
            Dictionary<string, object> metadataConstriants = new Dictionary<string, object>();
            metadataConstriants["PageName"] = pageName;

            return _compositionContext.TryGetExport(new CompositionContract(typeof(object), "OkraView", metadataConstriants), out view);
        }
    }
}